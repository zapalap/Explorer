using Explorer.Entities;
using Explorer.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Models
{
    public class FrameContext
    {
        public Intent LastPlayerAction { get; set; }
        public World World { get; set; }
    }
}
