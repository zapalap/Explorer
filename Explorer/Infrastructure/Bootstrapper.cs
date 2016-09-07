using Explorer.Controllers;
using Explorer.Entities;
using Explorer.Infrastructure.Fov;
using Explorer.Infrastructure.Helpers;
using Explorer.Infrastructure.Map;
using Explorer.Input;
using Explorer.Views;
using Iv;
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
        public static Container Container = new Container();

        public static Engine Start()
        {
            Container.RegisterInstance<ConsoleWindow>(c => new ConsoleWindow(80, 220, "Explorer"));
            Container.Register<IGraphicsComponent, AsciiGraphicsComponent>();

            Container.RegisterInstance<Player>(c =>
            {
                return new Player() { X = 10, Y = 15, EnergyDelta = 500 };
            });

            Container.Register<ITileFactory, NaturalCaveCaTileFactory>();

            Container.RegisterInstance<World>(c =>
            {
                var world = new World();
                world.Player = c.Resolve<Player>();
                world.Map = MapLoader.LoadMap(c.Resolve<ITileFactory>());
                return world;
            });

            Container.Register<IInputHandler, InputHandler>();
            Container.Register<IView<Creature>, MonsterView>();
            Container.Register<IView<Player>, PlayerView>();
            Container.Register<IView<Furniture>, MapView>();
            Container.Register<IView<GameLog>, GameLogView>();
            Container.Register<IFovStrategy, AlwaysVisibleFovStrategy>();
            
            Container.Register<PlayerController, PlayerController>();

            Container.RegisterInstance<GameLog>(c =>
            {
                return new GameLog { X = 1, Y = 60 };
            });

            Container.Register<ILogController, GameLogController>();
            Container.Register<IMoveHelper, EntityMoveHelper>();
            Container.Register<Engine, Engine>();

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

            var engine = Container.Resolve<Engine>();

            foreach (var monster in monsters)
            {
                var controller = new MonsterController(monster,
                    Container.Resolve<IView<Creature>>(),
                    Container.Resolve<World>(),
                    Container.Resolve<IMoveHelper>(), Container.Resolve<ILogController>());

                engine.AddActor(monster, controller);
            }

            engine.ActorControllers.Add(Container.Resolve<PlayerController>());

            foreach (var tile in engine.World.Map.Tiles)
            {
                engine.FurnitureControllers.Add(new MapController(tile.Furniture, Container.Resolve<IView<Furniture>>(), Container.Resolve<World>(), Container.Resolve<ILogController>()));
            }

            return engine;

        }
    }
}
