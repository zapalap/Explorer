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
    public class GameLogView : IView<GameLog>
    {
        private IGraphicsComponent Graphics;

        public GameLogView(IGraphicsComponent graphics)
        {
            Graphics = graphics;
        }

        public void Draw(GameLog model)
        {
            if (model.HasNew())
            {
                int x = 0;
                var messages = model.GetMessages();
                messages.Reverse();
                Color4 color = Color4.Green;
                foreach (var message in messages)
                {
                    if (x > 0)
                    {
                        color = message.Color;
                    }
                    Graphics.Write(model.X, model.Y + x++, message.Message, color);

                    if (x > 10)
                    {
                        break;
                    }
                }
            }
        }
    }
}
