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
    public class PlayerView : IView<Player>
    {
        private IGraphicsComponent Graphics;

        public PlayerView(IGraphicsComponent graphics)
        {
            Graphics = graphics;
        }

        public void Clear()
        {
            Graphics.Clear();
        }

        public void Clear(Player model)
        {
            Graphics.Clear();
        }

        public void Draw(Player player)
        {
            Graphics.Write(player.X, player.Y, "@", Color4.Yellow);
        }
    }
}
