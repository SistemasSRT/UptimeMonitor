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
                foreach (var mon in monitores)
                {
                    listaMonitor.Add(new SimpleMonitorDTO() { Nombre = mon.Nombre, Id = (ulong)mon.Id, Version = mon.Version, Estado = (int)mon.Estado });
                }
            }

            return listaMonitor;
        }

        // GET: api/Monitor/5
        public Model.Monitor Get(int id)
        {
            using (Model.Context db = new Model.Context())
            {
                return db.Monitors.First(x => x.Id == id);
            }
        }

        private void CambiarEstado(int id, Model.EstadoMonitorEnum estado) {
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
                    CambiarEstado(id, Model.EstadoMonitorEnum.Iniciado);
                    break;
                case "detener":
                    CambiarEstado(id, Model.EstadoMonitorEnum.Detenido);
                    break;
                case "limpiar":
                    break;
                default:
                    return NotFound();
            }

            return Ok();

        }

        // POST: api/Monitor
        public void Post(GuardadoMonitorDTO monitorDTO)
        {
            if (ModelState.IsValid)
            {
                var entidad = Mapper.Map<Model.Monitor>(monitorDTO);

                using (Model.Context db = new Model.Context())
                {
                    var monitor = db.Monitors.Where(m => m.Id == entidad.Id).FirstOrDefault();

                    if (monitor == null)
                    {
                        db.Monitors.Add(entidad);
                    }
                    else
                    {
                        monitor.Autenticacion = entidad.Autenticacion;
                        monitor.Descripcion = entidad.Descripcion;
                        monitor.Dominio = entidad.Dominio;
                        monitor.Intervalo = entidad.Intervalo;
                        monitor.IP = entidad.IP;
                        monitor.Nombre = entidad.Nombre;
                        monitor.Password = entidad.Password;
                        monitor.Puerto = entidad.Puerto;
                        monitor.Respuesta = entidad.Respuesta;
                        monitor.Tipo = entidad.Tipo;
                        monitor.URL = entidad.URL;
                        monitor.Usuario = entidad.Usuario;
                    }
                    
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
