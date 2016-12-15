using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;

namespace Server.Models
{
    public class FuncionFactory
    {
        //public static void EjecutarTarea(Model.Monitor monitor) {
        public static void EjecutarTarea(int monitorId)
        {
            Model.Monitor monitor = null;

            using (var db = new Model.Context())
            {
                monitor = db.Monitors.First(x => x.Id == monitorId);
                if (monitor.Estado == EstadoMonitorEnum.Detenido) return;
                switch (monitor.Tipo)
                {
                    case TipoMonitorEnum.Ip:
                        // return (monitorLocal) => {
                        try
                        {
                            var pingSender = new Ping();
                            PingOptions options = new PingOptions();

                            // Use the default Ttl value which is 128,
                            // but change the fragmentation behavior.
                            options.DontFragment = true;

                            // Create a buffer of 32 bytes of data to be transmitted.
                            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                            byte[] buffer = Encoding.ASCII.GetBytes(data);
                            int timeout = 120;
                            PingReply reply = pingSender.Send(monitor.IP, timeout, buffer, options);
                            MonitorLog monitorLog = new MonitorLog();
                            monitorLog.Resultado = reply.RoundtripTime.ToString();
                            monitorLog.Fecha = DateTime.Now;
                            monitorLog.Monitor = monitor;

                            if (reply.Status == IPStatus.Success)
                            {
                                monitorLog.TipoResultado = TipoResultado.Correcto;
                            }
                            else
                            {
                                monitorLog.TipoResultado = TipoResultado.Error;
                                monitorLog.Resultado = monitorLog.Resultado + " - Ip status: " + reply.Status.ToString();
                            }

                            db.MonitorLogs.Add(monitorLog);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                        break;
                    case TipoMonitorEnum.Url:
                        break;
                }

                }

        }
    }
}