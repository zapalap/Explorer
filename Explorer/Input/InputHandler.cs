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
                        frameContext.LastPlayerAction = MoveAction.MoveUp;
                        break;

                    case Key.S:
                        frameContext.LastPlayerAction = MoveAction.MoveDown;
                        break;

                    case Key.A:
                        frameContext.LastPlayerAction = MoveAction.MoveLeft;
                        break;

                    case Key.D:
                        frameContext.LastPlayerAction = MoveAction.MoveRight;
                        break;

                    default:
                        frameContext.LastPlayerAction = MoveAction.Idle;
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
