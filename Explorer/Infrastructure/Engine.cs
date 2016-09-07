using Explorer.Controllers;
using Explorer.Entities;
using Explorer.Infrastructure.Fov;
using Explorer.Infrastructure.Map;
using Explorer.Input;
using Explorer.Models;
using SunshineConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Infrastructure
{
    public class Engine
    {
        private IFovStrategy FovStrategy;
        private ITileFactory TileFactory;
        private IInputHandler InputHandler;
        private ConsoleWindow Console;

        public List<IController> ActorControllers { get; set; }
        public List<IController> FurnitureControllers { get; set; }
        public ILogController GameLogController { get; private set; }
        public World World { get; private set; }

        public Engine(IFovStrategy fovStrategy, ITileFactory tileFactory, World world, IInputHandler input, ILogController gameLog, ConsoleWindow console)
        {
            FovStrategy = fovStrategy;
            TileFactory = tileFactory;
            World = world;
            InputHandler = input;
            Console = console;

            if (ActorControllers == null)
            {
                ActorControllers = new List<IController>();
            }

            if (FurnitureControllers == null)
            {
                FurnitureControllers = new List<IController>();
            }

            GameLogController = gameLog;
        }

        public void AddActor(Creature creature, IController controller)
        {
            World.Creatures.Add(creature);
            ActorControllers.Add(controller);
        }

        public void AddFurniture(IController controller)
        {
            FurnitureControllers.Add(controller);
        }

        public bool WindowUpdate()
        {
            return Console.WindowUpdate();
        }

        public void Update(FrameContext frameContext)
        {
            HandleSystemCommands(frameContext);

            foreach (var actor in ActorControllers)
            {
                actor.Update(frameContext);
            }

            foreach (var furniture in FurnitureControllers)
            {
                furniture.Update(frameContext);
            }
        }

        public void Render()
        {
            foreach (var tile in World.Map.Tiles)
            {
                tile.Visible = false;
            }

            FovStrategy.SetVisibility(World);

            foreach (var furniture in FurnitureControllers)
            {
                furniture.Draw();
            }

            foreach (var creature in ActorControllers)
            {
                creature.Draw();
            }

            GameLogController.Draw();
        }

        private void HandleSystemCommands(FrameContext frameContext)
        {
            if (frameContext.LastPlayerAction == Enums.Intent.RegenerateMap)
            {
                World.Map = MapLoader.LoadMap(TileFactory);
            }
        }

        public bool HandleInput(FrameContext frameContext)
        {
            return InputHandler.Handle(frameContext);
        }
    }
}
