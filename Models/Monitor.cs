using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public enum TipoMonitorEnum { Url = 1, Ip = 2 }
    public enum EstadoMonitorEnum { Iniciado = 1, Detenido = 0 }
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class Monitor : ModelBase
    {
        public TipoMonitorEnum Tipo { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string URL { get; set; }

        public string IP { get; set; }

        public int? Puerto { get; set; }

        public string Respuesta { get; set; }

        public int Intervalo { get; set; }

        public string Usuario { get; set; }

        public string Password { get; set; }

        public string Dominio { get; set; }

        public Boolean Autenticacion { get; set; }

        public EstadoMonitorEnum Estado { get; set; } = EstadoMonitorEnum.Iniciado;

        public Monitor()
        {

        }
    }
}
