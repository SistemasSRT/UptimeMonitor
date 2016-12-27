using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Server.Controllers
{
    public class ModalesController : BaseMVCController
    {
        public ActionResult MonitorLog()
        {
            return View();
        }
    }
}