using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public class Context : DbContext
    {
        public DbSet<Monitor> Monitors { get; set; }

        public DbSet<MonitorLog> MonitorLogs { get; set; }

        public Context() : base("uptimeMonitor") {
            
        }
    }
}
