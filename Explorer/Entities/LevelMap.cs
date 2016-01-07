using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Entities
{
    public class LevelMap
    {
        public string Name { get; set; }
        public Tile[,] Tiles { get; set; }
    }
}
