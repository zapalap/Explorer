using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Entities;

namespace Explorer.Infrastructure.Map
{
    public class GeneratedTileFactory : ITileFactory
    {
        private Tile[,] Map;
        private Random Random;

        private const int mapWidth = 60;
        private const int mapHeight = 40;

        public GeneratedTileFactory()
        {
            Random = new Random();
        }

        public Tile[,] GetTiles()
        {
            InitializeMap();
            CreateRoom(1, 1, 6, 6);

            var wall = new Tile();

            for (int i = 0; i < 200; i++)
            {
                wall = PickAWall();

                if (wall == null)
                {
                    break;
                }

                CreateRoom(wall.X, wall.Y, Random.Next(3, 15), Random.Next(3, 15));
            }
                
            return Map;
        }

        private void InitializeMap()
        {
            Map = new Tile[mapWidth, mapHeight];

            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    Map[x, y] = new Tile() { Name = "MapTile", X=x, Y=y, Furniture = new Furniture() { Name = "Wall", Blocking = true, X = x, Y = y } };
                }
            }
        }

        private bool CreateRoom(int x, int y, int h, int w)
        {
            if (Map.GetLength(0) < x+w || Map.GetLength(1) < y+h)
            {
                return false;
            }

            for (int ulx = x; ulx < x+w; ulx++)
            {
                for (int uly = y; uly < y+h; uly++)
                {
                    if (!Map[ulx, uly].Furniture.Blocking)
                    {
                        return false;
                    }
                }
            }

            for (int ulx = x; ulx < x+w; ulx++)
            {
                for (int uly = y; uly < y+h; uly++)
                {
                    Map[ulx, uly] = new Tile() { Name = "Room", X=x, Y=y, Furniture = new Furniture() { Name = "Floor", Blocking = false, X = ulx, Y = uly } };
                }
            }

            return true;
        }

        private Tile PickAWall()
        {
            for (int i = 0; i < 1600; i++)
            {
                var x = Random.Next(1, mapWidth-1);
                var y = Random.Next(1, mapHeight-1);

                if (Map[x,y].Furniture.Blocking &&
                    (!Map[x + 1, y].Furniture.Blocking
                    || !Map[x - 1, y].Furniture.Blocking
                    || !Map[x, y + 1].Furniture.Blocking
                    || !Map[x, y - 1].Furniture.Blocking))
                    
                {
                    return Map[x, y];
                }
            }

            return null;
        }
    }
}
