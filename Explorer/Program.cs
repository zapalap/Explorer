using Explorer.Controllers;
using Explorer.Entities;
using Explorer.Infrastructure;
using Explorer.Infrastructure.Helpers;
using Explorer.Infrastructure.Map;
using Explorer.Input;
using Explorer.Models;
using Explorer.Views;
using SunshineConsole;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer
{
    class Program
    {
        static void Main(string[] args)
        {
            var console = new ConsoleWindow(80, 160, "Explorer");

            var graphics = new AsciiGraphicsComponent(console);

            var engine = new Engine();

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
                new Creature() { X = 53, Y = 20, EnergyDelta = 10 },
                new Creature() { X = 34, Y = 10, EnergyDelta = 100 },
                new Creature() { X = 48, Y = 20, EnergyDelta = 300 },
                new Creature() { X = 54, Y = 10, EnergyDelta = 100 },
                new Creature() { X = 12, Y = 20, EnergyDelta = 250 },
                new Creature() { X = 44, Y = 10, EnergyDelta = 100 },
                new Creature() { X = 39, Y = 20, EnergyDelta = 10 },
                new Creature() { X = 26, Y = 10, EnergyDelta = 100 },
                new Creature() { X = 20, Y = 20, EnergyDelta = 500 },
            };


            var player = new Player() { X = 10, Y = 15, EnergyDelta = 300 };
            var gameLog = new GameLog() { X = 1, Y = 60 };

            world.Player = player;
            world.Map = MapLoader.LoadMap(new TestMapFactory());

            var monsterView = new MonsterView(graphics);
            var playerView = new PlayerView(graphics);
            var furnitureView = new FurnitureView(graphics);
            var gameLogView = new GameLogView(graphics);

            var gameLogController = new GameLogController(gameLog, gameLogView, world);
            var moveHelper = new EntityMoveHelper(world, gameLogController);


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

            var frameContext = new FrameContext();
            var input = new InputHandler();

            var stopwatch = new Stopwatch();
            long lag = 0;

            while (console.WindowUpdate())
            {
                lag += stopwatch.ElapsedMilliseconds;

                stopwatch.Reset();
                stopwatch.Start();

                if (console.KeyPressed)
                {
                    input.Handle(frameContext, console.GetKey());
                }

                while (lag >= 16)
                {
                    engine.Update(frameContext);
                    lag -= 16;
                }

                engine.Render();
            }
        }
    }
}
