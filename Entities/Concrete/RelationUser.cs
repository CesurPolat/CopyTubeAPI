using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class RelationUser<T>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[]? ProfilePhoto { get; set; }
        public T Data { get; set; }
    }
}
