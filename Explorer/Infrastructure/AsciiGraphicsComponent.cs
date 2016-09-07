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

        public void Clear()
        {
            for (int x = 0; x < ConsoleWindow.Cols-1; x++)
            {
                for (int y = 0; y < ConsoleWindow.Rows - 1; y++)
                {
                    Write(x, y, " ", Color4.Black);
                }
            }
        }
    }
}
