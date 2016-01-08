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
        private const int MaxDisplayed = 20;

        public GameLogView(IGraphicsComponent graphics)
        {
            Graphics = graphics;
        }

        public void Draw(GameLog model)
        {
            if (model.HasNew())
            {
                ClearLog(model);
                int y = 0;
                var messages = model.GetMessages();
                var color = Color4.White;
                messages.Reverse();
                foreach (var message in messages)
                {
                    color = message.Color;
                    Graphics.Write(model.X, model.Y + y++, message.Message, color);

                    if (y > MaxDisplayed)
                    {
                        break;
                    }
                }
            }
        }

        private void ClearLog(GameLog model)
        {
            for (int y = 0; y <= MaxDisplayed; y++)
            {
                Graphics.Write(model.X, model.Y + y, "                                                                                                                        ", Color4.Black);
            }
        }
    }
}
