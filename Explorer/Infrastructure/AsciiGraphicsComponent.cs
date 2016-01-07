using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics;
using SunshineConsole;

namespace Explorer.Infrastructure
{
    class AsciiGraphicsComponent : IGraphicsComponent
    {
        private ConsoleWindow ConsoleWindow;

        public AsciiGraphicsComponent(ConsoleWindow console)
        {
            ConsoleWindow = console;
        }

        public void Write(int x, int y, string message, Color4 color)
        {
            ConsoleWindow.Write(y, x, message, color);
        }
    }
}
