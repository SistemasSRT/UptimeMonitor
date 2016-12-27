using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Server.Controllers
{
    public class TokenController : BaseMVCController
    {
        // GET: Token
        public ActionResult Index(string token)
        {
            Session.Add("token", token);
            return Redirect("/");
        }
    }
}