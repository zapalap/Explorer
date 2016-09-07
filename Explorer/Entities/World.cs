using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Entities
{
    public class World
    {

        public List<Creature> Creatures { get; set; }
        public Player Player { get; set; }
        public LevelMap Map { get; set; }

        public World()
        {
            Creatures = new List<Creature>();
            Map = new LevelMap();
        }
    }
}
