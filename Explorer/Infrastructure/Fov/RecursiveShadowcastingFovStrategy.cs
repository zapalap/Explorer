using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Entities;

namespace Explorer.Infrastructure.Fov
{
    class RecursiveShadowcastingFovStrategy : IFovStrategy
    {
        private const int VisualRange = 40;
        private World World;

        public void SetVisibility(World world)
        {
            World = world;
            for (int i = 0; i <= 8; i++)
            {
                ScanOctant(1, 1.0, 0.0, i);
            }
        }

        private void ScanOctant(int depth, double startSlope, double endSlope, int octant)
        {
            int x = 0;
            int y = 0;

            switch (octant)
            {

                case 1:
                    y = World.Player.Y - depth;
                    x = World.Player.X - Convert.ToInt32(startSlope * Convert.ToDouble(depth));

                    if (y < 0)
                    {
                        y = 0;
                    }

                    if (x < 0)
                    {
                        x = 0;
                    }

                    while (GetSlope(x, y, World.Player.X, World.Player.Y) >= endSlope)
                    {
                        if (GetDistance(x, y, World.Player.X, World.Player.Y) <= VisualRange * VisualRange)
                        {
                            if (World.Map.Tiles[x, y].Furniture.Blocking)
                            {
                                if (x - 1 >= 0 && !World.Map.Tiles[x - 1, y].Furniture.Blocking)
                                {
                                    ScanOctant(depth + 1, startSlope, GetSlope(x - 0.5, y + 0.5, World.Player.X, World.Player.Y), octant);
                                }
                            }
                            else
                            {
                                if (x - 1 >= 0 && World.Map.Tiles[x - 1, y].Furniture.Blocking)
                                {
                                    startSlope = GetSlope(x - 0.5, y - 0.5, World.Player.X, World.Player.Y);
                                }

                                World.Map.Tiles[x, y].Visible = true;
                            }
                        }
                        x++;
                    }
                    x--;
                    break;

                case 2:
                    y = World.Player.Y - depth;
                    x = World.Player.X + Convert.ToInt32(startSlope * Convert.ToDouble(depth));

                    if (y < 0)
                    {
                        y = 0;
                    }

                    if (x >= World.Map.Tiles.GetLength(0))
                    {
                        x = World.Map.Tiles.GetLength(0)-1;
                    }

                    while (GetSlope(x, y, World.Player.X, World.Player.Y) <= endSlope)
                    {
                        if (GetDistance(x, y, World.Player.X, World.Player.Y) <= VisualRange * VisualRange)
                        {
                            if (World.Map.Tiles[x, y].Furniture.Blocking)
                            {
                                if (x + 1 <= World.Map.Tiles.GetLength(0)-1 && !World.Map.Tiles[x + 1, y].Furniture.Blocking)
                                {
                                    ScanOctant(depth + 1, startSlope, GetSlope(x + 0.5, y + 0.5, World.Player.X, World.Player.Y), octant);
                                }
                            }
                            else
                            {
                                if (x + 1 <= World.Map.Tiles.GetLength(0)-1 && World.Map.Tiles[x + 1, y].Furniture.Blocking)
                                {
                                    startSlope = -GetSlope(x + 0.5, y - 0.5, World.Player.X, World.Player.Y);
                                }

                                World.Map.Tiles[x, y].Visible = true;
                            }
                        }
                        x--;
                    }
                    x++;
                    break;

                case 3:

                    break;

                default:
                    break;
            }

            if (x>=World.Map.Tiles.GetLength(0))
            {
                x = World.Map.Tiles.GetLength(0) - 1;
            }
            if (depth < VisualRange && !World.Map.Tiles[x, y].Furniture.Blocking)
            {
                ScanOctant(depth + 1, startSlope, endSlope, octant);
            }
        }

        private int GetDistance(int x1, int y1, int x2, int y2)
        {
            return (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
        }

        private double GetSlope(double x1, double y1, double x2, double y2)
        {
            return (x1 - x2) / (y1 - y2);
        }
    }
}
