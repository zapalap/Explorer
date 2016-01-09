using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Entities;

namespace Explorer.Infrastructure.Map
{
    public class TestMapFactory : ITileFactory
    {
        public Tile[,] GetTiles()
        {
            var tiles = new Tile[40, 40];
            var rnd = new Random();

            for (int x = 0; x < 40; x++)
            {
                for (int y = 0; y < 40; y++)
                {
                    int r = rnd.Next(0, 10);
                    var blocking =  r < 9 ? false : true;
                    tiles[x, y] = new Tile() { Name = $"Tile[{x},{y}]", Furniture = new Furniture() { Name = "Test furniture", Blocking = blocking, X = x, Y = y} };
                }
            }

            return tiles;
        }
    }
}
