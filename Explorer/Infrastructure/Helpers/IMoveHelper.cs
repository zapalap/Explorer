using Explorer.Entities;
using Explorer.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Infrastructure.Helpers
{
    public interface IMoveHelper
    {
        bool Move(BaseEntity entity, MoveAction action);
    }
}
