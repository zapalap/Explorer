using Explorer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Infrastructure.Map
{
    public interface ITileFactory
    {
        Tile[,] GetTiles();
    }
}
