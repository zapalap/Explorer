using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Entities;

namespace Explorer.Infrastructure.Fov
{
    public class AlwaysVisibleFovStrategy : IFovStrategy
    {
        public void SetVisibility(World world)
        {
            for (int x = 0; x < world.Map.Tiles.GetLength(0); x++)
            {
                for (int y = 0; y < world.Map.Tiles.GetLength(1); y++)
                {
                    world.Map.Tiles[x, y].Visible = true;
                }
            }
        }
    }
}
