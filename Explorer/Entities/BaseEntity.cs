using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Entities
{
    public class BaseEntity
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int OldX { get; set; }
        public int OldY { get; set; }
        public int EnergyDelta { get; set; }
        public int Energy { get; set; }

        public bool IsReadyToAct()
        {
            if (Energy >= 500)
            {
                Energy = 0;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddEnergy()
        {
            Energy += EnergyDelta;
        }
    }
}
