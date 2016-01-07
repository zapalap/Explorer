using Explorer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Infrastructure.Helpers
{
    public class EnergyHelper
    {
        private const int EnergyThreshold = 5000;

        public bool HasEnough(BaseEntity entity)
        {
            return entity.Energy >= EnergyThreshold;
        }


    }
}
