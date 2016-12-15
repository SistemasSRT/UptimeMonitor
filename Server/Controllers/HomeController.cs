using Hangfire;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Server.Controllers
{
    public class HomeController : BaseMVCController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            try
            {
                var pingSender = new Ping();
                PingOptions options = new PingOptions();

                // Use the default Ttl value which is 128,
                // but change the fragmentation behavior.
                options.DontFragment = true;

                var monitor = new Model.Monitor();
                using(Model.Context db = new Model.Context())
                {
                    monitor = db.Monitors.First(x => x.Id == 5);
                }

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

                using (Model.Context db = new Model.Context())
                {
                    db.MonitorLogs.Add(monitorLog);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return View();
        }
    }
}
