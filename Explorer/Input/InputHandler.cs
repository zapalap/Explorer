using Explorer.Infrastructure.Enums;
using Explorer.Models;
using OpenTK.Input;
using SunshineConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Input
{
    public class InputHandler : IInputHandler
    {
        private ConsoleWindow Console;

        public InputHandler(ConsoleWindow console)
        {
            Console = console;
        }

        public bool Handle(FrameContext frameContext)
        {
            if (Console.KeyPressed)
            {
                switch (Console.GetKey())
                {
                    case Key.W:
                        frameContext.LastPlayerAction = Intent.MoveUp;
                        break;

                    case Key.S:
                        frameContext.LastPlayerAction = Intent.MoveDown;
                        break;

                    case Key.A:
                        frameContext.LastPlayerAction = Intent.MoveLeft;
                        break;

                    case Key.D:
                        frameContext.LastPlayerAction = Intent.MoveRight;
                        break;

                    case Key.R:
                        frameContext.LastPlayerAction = Intent.RegenerateMap;
                        break;

                    default:
                        frameContext.LastPlayerAction = Intent.Idle;
                        break;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
