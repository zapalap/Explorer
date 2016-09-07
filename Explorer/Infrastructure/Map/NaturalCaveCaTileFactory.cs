using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Entities;

namespace Explorer.Infrastructure.Map
{
    public class NaturalCaveCaTileFactory : ITileFactory
    {
        private Tile[,] Map;
        private Random Random;

        private const int mapWidth = 260;
        private const int mapHeight = 60;

        private const int iterations = 5;

        public NaturalCaveCaTileFactory()
        {
            Random = new Random();
        }

        private void InitializeMap()
        {
            Map = new Tile[mapWidth, mapHeight];

            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    var rnd = Random.Next(0, 10);
                    if (rnd >= 6)
                    {
                        Map[x, y] = new Tile() { Name = "MapTile", X = x, Y = y, Furniture = new Furniture() { Name = "Wall", Blocking = true, X = x, Y = y } };
                    }
                    else
                    {
                        Map[x, y] = new Tile() { Name = "Room", X = x, Y = y, Furniture = new Furniture() { Name = "Floor", Blocking = false, X = x, Y = y } };
                    }
                }
            }
        }

        public Tile[,] GetTiles()
        {
            InitializeMap();

            for (int i = 0; i <= iterations; i++)
            {
                ApplyRules();
            }

            return Map;
        }

        private bool IsAWall(int x, int y)
        {
            if (Map[x, y]?.Furniture == null)
            {
                return false;
            }

            return Map[x, y].Furniture.Name == "Wall";
        }

        private Tile GenerateTile(int neighboringWallCount, int x, int y)
        {
            if (IsAWall(x, y) && neighboringWallCount >= 4)
            {
                return new Tile() { Name = "MapTile", X = x, Y = y, Furniture = new Furniture() { Name = "Wall", Blocking = true, X = x, Y = y } };
            }

            if (!IsAWall(x, y) && neighboringWallCount >= 5)
            {
                return new Tile() { Name = "MapTile", X = x, Y = y, Furniture = new Furniture() { Name = "Wall", Blocking = true, X = x, Y = y } };
            }

            return new Tile() { Name = "Room", X = x, Y = y, Furniture = new Furniture() { Name = "Floor", Blocking = false, X = x, Y = y } };
        }

        private bool IsValidMove(int x, int y)
        {
            if (x > mapWidth - 1)
            {
                return false;
            }

            if (x < 0)
            {
                return false;
            }

            if (y > mapHeight)
            {
                return false;
            }

            if (y < 0)
            {
                return false;
            }

            return true;
        }


        private int GetNeighboringWalls(int x, int y)
        {
            int neighbors = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (IsValidMove(x + i, y + j))
                    {
                        neighbors += IsAWall(x + i, y + j) ? 1 : 0;
                    }
                }
            }

            return neighbors;
        }

        public void ApplyRules()
        {
            for (int x = 0; x < mapWidth - 1; x++)
            {
                for (int y = 0; y < mapHeight - 1; y++)
                {
                    Map[x, y] = GenerateTile(GetNeighboringWalls(x, y), x, y);
                }
            }
        }
    }
}
