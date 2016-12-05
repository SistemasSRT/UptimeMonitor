using Server.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Server.Controllers
{
    public class MonitorController : ApiController
    {
        // GET: api/Monitor
        public IEnumerable<SimpleMonitorDTO> Get()
        {
            return new List<SimpleMonitorDTO>();
        }

        // GET: api/Monitor/5
        public SimpleMonitorDTO Get(int id)
        {
            return new SimpleMonitorDTO();
        }

        // POST: api/Monitor
        public void Post(GuardadoMonitorDTO monitor)
        {
            if (ModelState.IsValid)
            {

            }
        }
    }
}
