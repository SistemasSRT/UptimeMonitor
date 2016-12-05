using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Server.Models.DTOs
{
    public class GuardadoMonitorDTO : DTOs.BaseDTO
    {
        [Required]
        public int Tipo { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [Url]
        public string URL { get; set; }

        //[RegularExpression()]    
        public string IP { get; set; }

        public int Puerto { get; set; }

        public string Respuesta { get; set; }

        [Required]
        public int Intervalo { get; set; }

        public string Usuario { get; set; }

        public string Password { get; set; }

        public string Dominio { get; set; }

        public Boolean Autenticacion { get; set; }
    }
}