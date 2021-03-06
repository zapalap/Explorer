﻿using Explorer.Controllers;
using Explorer.Entities;
using Explorer.Infrastructure;
using Explorer.Infrastructure.Fov;
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
            var engine = Bootstrapper.Start();

            var frameContext = new FrameContext() { World = engine.World };

            while (engine.WindowUpdate())
            {
                if (engine.HandleInput(frameContext))
                {
                    engine.Update(frameContext);
                }

                engine.Render();
            }
        }
    }
}
