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
    public class MonsterView : IView<Creature>
    {
        private IGraphicsComponent Graphics;

        public MonsterView(IGraphicsComponent graphics)
        {
            Graphics = graphics;
        }

        public void Clear()
        {
            Graphics.Clear();
        }

        public void Clear(Creature model)
        {
            Graphics.Clear();
        }

        public void Draw(Creature model)
        {
            if (model.Visible)
            {
                Graphics.Write(model.X, model.Y, "M", Color4.PaleVioletRed);
            }
        }
    }
}
