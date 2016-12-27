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
using Model;

namespace Server.Controllers
{
    public class MonitorController : BaseAPIController
    {
        // GET: api/Monitor
        public IHttpActionResult Get()
        {
            List<SimpleMonitorDTO> listaMonitor = new List<SimpleMonitorDTO>();

            using (Context db = new Model.Context())
            {
                var monitores = db.Monitors.ToList();
                foreach (var mon in monitores)
                {
                    var dto = Mapper.Map<SimpleMonitorDTO>(mon);

                    /*var ultimo = db.MonitorLogs.Where(m => m.Monitor.Id == mon.Id).OrderByDescending(m => m.Fecha).ToList().FirstOrDefault();

                    if (ultimo != null)
                        dto.UltimaEjecucion = string.Format("{0:dd/MM/yyyy HH:mm:ss} - {1}", ultimo.Fecha, ultimo.Resultado);
                        */
                    listaMonitor.Add(dto);
                }
            }

            return Ok(listaMonitor);
        }

        // GET: api/Monitor/5
        public Model.Monitor Get(int id)
        {
            using (Model.Context db = new Model.Context())
            {
                return db.Monitors.FirstOrDefault(x => x.Id == id);
            }
        }

        private void CambiarEstado(int id, Model.EstadoMonitorEnum estado, Model.Context db)
        {
            db.Monitors.First(x => x.Id == id).Estado = estado;
            db.SaveChanges();
        }

        [Route("api/monitor/{id}/{accion}")]
        [HttpPost]
        public IHttpActionResult Accion(int id, string accion)
        {
            using (Model.Context db = new Model.Context())
            {
                switch (accion)
                {
                    case "iniciar":
                        CambiarEstado(id, Model.EstadoMonitorEnum.Iniciado, db);
                        break;
                    case "detener":
                        CambiarEstado(id, Model.EstadoMonitorEnum.Detenido, db);
                        break;
                    case "limpiar":
                        LimpiarLogs(id, db);
                        return Ok(new List<SimpleMonitorDTO>());                                            
                    default:
                        return NotFound();
                }
            }

            return Get();
        }

        private void LimpiarLogs(int id, Context db)
        {
            db.MonitorLogs.RemoveRange(db.MonitorLogs.Where(x => x.Monitor.Id == id).ToList());
            db.SaveChanges();
        }

        private void Eliminar(int id, Model.Context db)
        {
            Hangfire.RecurringJob.RemoveIfExists("tarea_" + id.ToString());
            db.MonitorLogs.RemoveRange(db.MonitorLogs.Where(x => x.Monitor.Id == id).ToList());
            db.Monitors.Remove(db.Monitors.First(x => x.Id == id));
            db.SaveChanges();            
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
                        Mapper.Map(entidad, monitor, entidad.GetType(), monitor.GetType());
                    }

                    db.SaveChanges();
                }

                RecurringJob.
                AddOrUpdate("tarea_" + entidad.Id.ToString(),
                    () => FuncionFactory.EjecutarTarea(entidad.Id),
                    Cron.MinuteInterval(entidad.Intervalo));
            }
        }

        public IHttpActionResult Delete(int id)
        {
            using (Model.Context db = new Model.Context())
            {
                Eliminar(id, db);
            }

            return Get();
        }
    }
}
