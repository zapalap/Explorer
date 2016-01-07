using Explorer.Infrastructure.Enums;
using Explorer.Models;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Input
{
    public class InputHandler
    {
        public void Handle(FrameContext frameContext, Key key)
        {
            switch (key)
            {
                case Key.W: frameContext.LastPlayerAction = MoveAction.MoveUp;
                    break;

                case Key.S: frameContext.LastPlayerAction = MoveAction.MoveDown;
                    break;

                case Key.A: frameContext.LastPlayerAction = MoveAction.MoveLeft;
                    break;

                case Key.D: frameContext.LastPlayerAction = MoveAction.MoveRight;
                    break;

                default:
                    frameContext.LastPlayerAction = MoveAction.Idle;
                    break;
            }
        }
    }
}
