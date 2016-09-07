using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Controllers
{
    public interface ILogController
    {
        void AddMessage(string message);
        void AddMessage(string message, Color4 color);
        void Draw();
    }
}
