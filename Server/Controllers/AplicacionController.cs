using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Server.Controllers
{
    public class AplicacionController : BaseMVCController
    {
        // GET: Aplicacion
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Monitor()
        {
            return View();
        }
        public ActionResult Monitores()
        {
            return View();
        }
        public ActionResult Grafico()
        {        
            return View();
        }
    }
}