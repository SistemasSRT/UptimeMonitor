using Model;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;

namespace Server.Models
{
    public class FuncionFactory
    {
        private static readonly Dictionary<TipoMonitorEnum, IEjecutable> _tests;

        static FuncionFactory()
        {
            _tests = new Dictionary<TipoMonitorEnum, IEjecutable>();
            _tests.Add(TipoMonitorEnum.Ip, new PingTest());
            _tests.Add(TipoMonitorEnum.Url, new HttpTest());
        }

        public static void EjecutarTarea(int monitorId)
        {
            Model.Monitor monitor = null;

            using (var db = new Model.Context())
            {
                monitor = db.Monitors.FirstOrDefault(x => x.Id == monitorId);

                if (monitor.Estado == EstadoMonitorEnum.Detenido) return;

                if (_tests.ContainsKey(monitor.Tipo))
                {
                    var monitorLog = _tests[monitor.Tipo].Ejecutar(monitor);
                    monitorLog.Monitor = monitor;
                    db.MonitorLogs.Add(monitorLog);
                    db.SaveChanges();
                }
            }
        }
       
        private class PingTest : IEjecutable
        {
            public MonitorLog Ejecutar(Model.Monitor monitor)
            {
                MonitorLog monitorLog = new MonitorLog();
                
                var pingSender = new Ping();
                PingOptions options = new PingOptions();

                // Use the default Ttl value which is 128,
                // but change the fragmentation behavior.
                options.DontFragment = true;

                // Create a buffer of 32 bytes of data to be transmitted.
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;
                try
                {
                    PingReply reply = pingSender.Send(monitor.IP, timeout, buffer, options);

                    monitorLog.Resultado = reply.RoundtripTime.ToString() + " ms";

                    if (reply.Status == IPStatus.Success)
                    {
                        monitorLog.TipoResultado = TipoResultado.Correcto;
                    }
                    else
                    {
                        monitorLog.TipoResultado = TipoResultado.Error;
                        monitorLog.Resultado = reply.Status.ToString();
                    }
                }
                catch (Exception e)
                {
                    monitorLog.TipoResultado = TipoResultado.Error;
                    monitor.Respuesta = e.Message;
                }

                return monitorLog;
            }
        }

        private class HttpTest : IEjecutable
        {
            public MonitorLog Ejecutar(Model.Monitor monitor)
            {
                var monitorLog = new MonitorLog();
                
                var client = new RestClient(monitor.URL);

                if (monitor.Autenticacion)
                    client.Authenticator = new HttpBasicAuthenticator(monitor.Dominio + "\\" + monitor.Usuario, monitor.Password);

                var request = new RestRequest(Method.GET);

                try
                {
                    var response = client.Execute(request);

                    monitorLog.Resultado = (int)response.StatusCode + " - " + response.StatusDescription;

                    if (string.IsNullOrEmpty(monitor.Respuesta))
                    {
                        monitorLog.TipoResultado = ((int)response.StatusCode >= 200 && (int)response.StatusCode < 300) ? TipoResultado.Correcto : TipoResultado.Error;
                    }
                    else
                    {
                        monitorLog.TipoResultado = (monitor.Respuesta.Equals(response.Content)) ? TipoResultado.Correcto : TipoResultado.Error;
                        monitorLog.Resultado += " - " + response.Content;
                    }
                }
                catch (Exception e)
                {
                    monitorLog.TipoResultado = TipoResultado.Error;
                    monitor.Respuesta = e.Message;
                }


                return monitorLog;
            }
        }
    }

    public interface IEjecutable
    {
        MonitorLog Ejecutar(Model.Monitor monitor);
    }    
}