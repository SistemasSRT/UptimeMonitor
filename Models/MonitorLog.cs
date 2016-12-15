using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MonitorLog : ModelBase
    {
        public Monitor Monitor { get; set; }

        public DateTime Fecha { get; set; }

        public TipoResultado TipoResultado { get; set; }

        public String Resultado { get; set; }
    }

    public enum TipoResultado
    {
        Error,
        Correcto
    }
}
