using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Models.DTOs
{
    public class BaseDTO
    {
        public Guid Version { get; set; }

        public ulong OID { get; set; }
    }
}