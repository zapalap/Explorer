using Explorer.Entities;
using Explorer.Infrastructure;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Views
{
    public class FurnitureView : IView<Furniture>
    {
        private IGraphicsComponent Graphics;

        public FurnitureView(IGraphicsComponent graphics)
        {
            Graphics = graphics;
        }

        public void Draw(Furniture model)
        {
            if (model.Blocking)
            {
                Graphics.Write(model.X, model.Y, "#", Color4.LightGray);
            }
            else
            {
                Graphics.Write(model.X, model.Y, ".", Color4.LightBlue);
            }
        }
    }
}
