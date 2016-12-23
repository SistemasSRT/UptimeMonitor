using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Model;

namespace Server.Controllers
{
    public class MonitorLogsController : ApiController
    {
        private Context db = new Context();

        // GET: api/MonitorLogs
        public IQueryable<MonitorLog> GetMonitorLogs()
        {
            return db.MonitorLogs;
        }

        // GET: api/MonitorLogs/5
        [ResponseType(typeof(MonitorLog))]
        public IHttpActionResult GetMonitorLog(int id)
        {
            MonitorLog monitorLog = db.MonitorLogs.Find(id);
            if (monitorLog == null)
            {
                return NotFound();
            }

            return Ok(monitorLog);
        }

        // PUT: api/MonitorLogs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMonitorLog(int id, MonitorLog monitorLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != monitorLog.Id)
            {
                return BadRequest();
            }

            db.Entry(monitorLog).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonitorLogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/MonitorLogs
        [ResponseType(typeof(MonitorLog))]
        public IHttpActionResult PostMonitorLog(MonitorLog monitorLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MonitorLogs.Add(monitorLog);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = monitorLog.Id }, monitorLog);
        }

        // DELETE: api/MonitorLogs/5
        [ResponseType(typeof(MonitorLog))]
        public IHttpActionResult DeleteMonitorLog(int id)
        {
            MonitorLog monitorLog = db.MonitorLogs.Find(id);
            if (monitorLog == null)
            {
                return NotFound();
            }

            db.MonitorLogs.Remove(monitorLog);
            db.SaveChanges();

            return Ok(monitorLog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MonitorLogExists(int id)
        {
            return db.MonitorLogs.Count(e => e.Id == id) > 0;
        }

        [HttpGet]
        [Route("api/monitorlogs/monitor/{monitorId}/{pagina}/{cantidad}")]
        public IHttpActionResult ObtenerListaLogPorMonitor(int monitorId, int pagina, int cantidad)
        {
            return Ok(db.MonitorLogs.Where(x => x.Monitor.Id == monitorId)
                .OrderByDescending(x => x.Fecha)
                .Skip(pagina * cantidad)
                .Take(cantidad).ToList());
        }
    }
}