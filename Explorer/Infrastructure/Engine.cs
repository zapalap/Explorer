using Explorer.Controllers;
using Explorer.Entities;
using Explorer.Infrastructure.Fov;
using Explorer.Input;
using Explorer.Models;
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
        private IInputHandler InputHandler;

        public List<IController> ActorControllers { get; set; }
        public List<IController> FurnitureControllers { get; set; }
        public IController GameLogController { get; set; }
        public World World { get; private set; }

        public Engine(IFovStrategy fovStrategy, World world, IInputHandler input)
        {
            FovStrategy = fovStrategy;
            World = world;
            InputHandler = input;

            if (ActorControllers == null)
            {
                ActorControllers = new List<IController>();
            }

            if (FurnitureControllers == null)
            {
                FurnitureControllers = new List<IController>();
            }
        }

        public void Update(FrameContext frameContext)
        {
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

        public bool HandleInput(FrameContext frameContext)
        {
            return InputHandler.Handle(frameContext);
        }
    }
}
