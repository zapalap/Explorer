using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Infrastructure
{
    public interface IGraphicsComponent
    {
        void Write(int x, int y, string message, Color4 color);
        void Clear();
    }
}
