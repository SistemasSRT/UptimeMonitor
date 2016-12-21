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
            return View();
        }
    }
}
