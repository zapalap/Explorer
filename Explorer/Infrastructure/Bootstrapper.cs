using Explorer.Controllers;
using Explorer.Entities;
using Explorer.Infrastructure.Fov;
using Explorer.Infrastructure.Helpers;
using Explorer.Infrastructure.Map;
using Explorer.Input;
using Explorer.Views;
using SunshineConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Infrastructure
{
    public static class Bootstrapper
    {
        public static Engine Start(ConsoleWindow console)
        {
            var graphics = new AsciiGraphicsComponent(console);

            var world = new World();
            var monsters = new List<Creature>()
            {
                new Creature() { X = 15, Y = 10, EnergyDelta = 100 },
                new Creature() { X = 23, Y = 20, EnergyDelta = 250 },
                new Creature() { X = 10, Y = 10, EnergyDelta = 100 },
                new Creature() { X = 29, Y = 20, EnergyDelta = 10 },
                new Creature() { X = 18, Y = 10, EnergyDelta = 100 },
                new Creature() { X = 31, Y = 20, EnergyDelta = 100 },
                new Creature() { X = 10, Y = 10, EnergyDelta = 100 },
                new Creature() { X = 38, Y = 20, EnergyDelta = 250 },
                new Creature() { X = 10, Y = 10, EnergyDelta = 100 },
                new Creature() { X = 15, Y = 20, EnergyDelta = 10 },
                new Creature() { X = 34, Y = 10, EnergyDelta = 100 },
                new Creature() { X = 18, Y = 20, EnergyDelta = 300 },
                new Creature() { X = 12, Y = 10, EnergyDelta = 100 },
                new Creature() { X = 12, Y = 20, EnergyDelta = 250 },
                new Creature() { X = 23, Y = 10, EnergyDelta = 100 },
                new Creature() { X = 39, Y = 20, EnergyDelta = 10 },
                new Creature() { X = 26, Y = 10, EnergyDelta = 100 },
                new Creature() { X = 20, Y = 20, EnergyDelta = 500 },
            };

            var player = new Player() { X = 10, Y = 15, EnergyDelta = 500 };
            var gameLog = new GameLog() { X = 1, Y = 40 };

            //world.Creatures = monsters;
            world.Player = player;
            world.Map = MapLoader.LoadMap(new NaturalCaveCaTileFactory());

            var monsterView = new MonsterView(graphics);
            var playerView = new PlayerView(graphics);
            var furnitureView = new FurnitureView(graphics);
            var gameLogView = new GameLogView(graphics);

            //var fovStrategy = new RecursiveShadowcastingFovStrategy();

            var fovStrategy = new AlwaysVisibleFovStrategy();
            var input = new InputHandler(console);

            var gameLogController = new GameLogController(gameLog, gameLogView, world);
            var moveHelper = new EntityMoveHelper(world, gameLogController);

            var engine = new Engine(fovStrategy, world, input);

            engine.GameLogController = gameLogController;

            foreach (var monster in monsters)
            {
                world.Creatures.Add(monster);
                engine.ActorControllers.Add(new MonsterController(monster, monsterView, world, moveHelper, gameLogController));
            }

            engine.ActorControllers.Add(new PlayerController(player, playerView, world, moveHelper, gameLogController));


            foreach (var tile in world.Map.Tiles)
            {
                engine.FurnitureControllers.Add(new FurnitureController(tile.Furniture, furnitureView, world, gameLogController));
            }

            return engine;

        }
    }
}
