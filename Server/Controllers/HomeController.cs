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
                return Redirect("http://localhost:55749/Login?client_id=9cae8e3f-1961-41f4-92e9-44b9c283474b&redirect_uri=http://localhost:51388/Token");
            }

            return View();
        }
    }
}
