﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Entities
{
    public class Furniture : BaseEntity
    {
        public string Name { get; set; }
        public bool Blocking { get; set; }
    }
}
