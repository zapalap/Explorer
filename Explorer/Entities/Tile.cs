using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Entities
{
    public class Tile
    {
        public string Name { get; set; }
        public BaseEntity Occupant { get; set; }
        public Furniture Furniture { get; set; }
    }
}
