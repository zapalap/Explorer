using Explorer.Controllers;
using Explorer.Entities;
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
        public List<IController> ActorControllers { get; set; }
        public List<IController> FurnitureControllers { get; set; }
        public IController GameLogController { get; set; }

        public Engine()
        {
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
    }
}
