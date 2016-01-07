using Explorer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Infrastructure.Map
{
    public class MapLoader
    {
        public static LevelMap LoadMap(ITileFactory tileFactory)
        {
            var levelMap = new LevelMap() { Name = "Test Map", Tiles = tileFactory.GetTiles()};
            return levelMap;
        }
    }
}
