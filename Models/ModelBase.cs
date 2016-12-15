using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model
{
    public abstract class ModelBase
    {
        public Guid Version { get; set; }

        public int Id { get; set; }
    }
}
