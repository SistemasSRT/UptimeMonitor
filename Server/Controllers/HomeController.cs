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
            ViewBag.Token = Session["token"];
            if (string.IsNullOrEmpty(ViewBag.Token))
            {
                return Redirect("http://localhost:50615/Login/Login");
            }

            return View();
        }
    }
}
