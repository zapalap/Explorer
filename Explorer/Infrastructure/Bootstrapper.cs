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
            Container
                .For<ConsoleWindow>()
                .Provide(c => new ConsoleWindow(80, 220, "Explorer"))
                .AsSingleton();

            Container
                .For<IGraphicsComponent>()
                .Provide<AsciiGraphicsComponent>();

            Container
                .For<Player>()
                .Provide(c =>
                {
                    return new Player() { X = 10, Y = 15, EnergyDelta = 500 };
                }).AsSingleton();

            Container.For<ITileFactory>().Provide<NaturalCaveCaTileFactory>();

            Container.For<World>()
                .Provide(c =>
                {
                    var world = new World();
                    world.Player = c.Resolve<Player>();
                    world.Map = MapLoader.LoadMap(c.Resolve<ITileFactory>());
                    return world;
                }).AsSingleton();

            Container
                .For<IInputHandler>()
                .Provide<InputHandler>();

            Container
                .For<IView<Creature>>()
                .Provide<MonsterView>();

            Container
                .For<IView<Player>>()
                .Provide<PlayerView>();

            Container
                .For<IView<Furniture>>()
                .Provide<MapView>();

            Container
                .For<IView<GameLog>>()
                .Provide<GameLogView>();

            Container
                .For<IFovStrategy>()
                .Provide<AlwaysVisibleFovStrategy>();

            Container
                .For<PlayerController>()
                .Provide<PlayerController>();

            Container
                .For<GameLog>()
                .Provide(c => new GameLog { X = 1, Y = 60 })
                .AsSingleton();

            Container
                .For<ILogController>()
                .Provide<GameLogController>();

            Container
                .For<IMoveHelper>()
                .Provide<EntityMoveHelper>();

            Container
                .For<Engine>()
                .Provide<Engine>()
                .AsSingleton();

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
