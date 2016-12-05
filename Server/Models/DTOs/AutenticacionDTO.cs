using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Models.DTOs
{
    public class AutenticacionDTO : DTOs.BaseDTO
    {
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string Dominio { get; set; }
    }
}