using AutoMapper;
using Hangfire;
using Server.Models;
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
            List<SimpleMonitorDTO> listaMonitor = new List<SimpleMonitorDTO>();

            using (Model.Context db = new Model.Context())
            {
                var monitores = db.Monitors.ToList();
                foreach (var mon in monitores) {
                    listaMonitor.Add(new SimpleMonitorDTO() { Nombre = mon.Nombre, OID = (ulong)mon.Id, Version = mon.Version, Estado = (int)mon.Estado });
                }
            }


            return listaMonitor;
        }

        // GET: api/Monitor/5
        public SimpleMonitorDTO Get(int id)
        {
            return new SimpleMonitorDTO();
        }
        private void cambiarEstado(int id, Model.EstadoMonitorEnum estado) {
            using (Model.Context db = new Model.Context())
            {
                db.Monitors.First(x => x.Id == id).Estado = estado;
                db.SaveChanges();
            }
        }
        [Route("api/monitor/{id}/{accion}")]
        [HttpPost]
        public IHttpActionResult Accion(int id, string accion)
        {
            switch (accion)
            {
                case "iniciar":
                    cambiarEstado(id, Model.EstadoMonitorEnum.Iniciado);
                    break;
                case "detener":
                    cambiarEstado(id, Model.EstadoMonitorEnum.Detenido);
                    break;
                case "limpiar":
                    break;
                default:
                    return NotFound();
            }

            return Ok();

        }

        // POST: api/Monitor
        public void Post(GuardadoMonitorDTO monitor)
        {
            if (ModelState.IsValid)
            {
                var entidad = Mapper.Map<Model.Monitor>(monitor);

                using (Model.Context db = new Model.Context())
                {
                    db.Monitors.Add(entidad);
                    db.SaveChanges();
                }

                RecurringJob.
                AddOrUpdate("tarea_" + entidad.Id.ToString(),
                    () => FuncionFactory.EjecutarTarea(entidad.Id),
                    Cron.MinuteInterval(entidad.Intervalo));

            }
        }
    }
}
