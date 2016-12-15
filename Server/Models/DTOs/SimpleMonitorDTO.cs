using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Models.DTOs
{
    public class SimpleMonitorDTO : BaseDTO
    {
        public String Nombre { get; set; }
        public int Estado { get; set; }
    }
}