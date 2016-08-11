using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Entities;
using System.Drawing;

namespace Explorer.Infrastructure.Fov
{
    public class RayCastFovStrategy : IFovStrategy
    {
        private World World;

        public void SetVisibility(World world)
        {
            World = world;
            var area = new Rectangle(0, 0, world.Map.Tiles.GetLength(0), world.Map.Tiles.GetLength(1));

            for (int x = area.Left; x < area.Right; x++)
            {
                TraceLine(world.Player.X, world.Player.Y, x, area.Top);
                TraceLine(world.Player.X, world.Player.Y, x, area.Bottom);

            }

            for (int y = 0; y < area.Top + 1; y++)
            {
                TraceLine(world.Player.X, world.Player.Y, area.Left, y);
                TraceLine(world.Player.X, world.Player.Y, area.Right, y);
            }
        }

        public void TraceLine(int x1, int y1, int x2, int y2)
        {
            double dx = x2 - x1;
            double dy = y2 - y1;
            double error = 0;
            double derror = Math.Abs(dy / dx);
            int y = y1;

            for (int x = x1; x < x2; x++)
            {
                if (!IsBlocking(x, y))
                {
                    SetVisible(x, y);
                }
                else
                {
                    break;
                }

                error += error + derror;

                while (error >= 0.5)
                {
                    if (!IsBlocking(x, y))
                    {
                        SetVisible(x, y);
                    }

                    y += y2 - y1 > 0 ? 1 : 0;
                    error -= 1.0;

                }
            }
        }

        private void SetVisible(int x, int y)
        {
            World.Map.Tiles[x, y].Visible = true;
        }

        private bool IsBlocking(int x, int y)
        {
            return World.Map.Tiles[x, y].Furniture.Blocking;
        }

    }
}
