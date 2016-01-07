using Explorer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Models;
using Explorer.Views;
using OpenTK.Graphics;

namespace Explorer.Controllers
{
    public class GameLogController : BaseController<GameLog>, ILogController
    {
        public GameLogController(GameLog gameLog, IView<GameLog> view, World world) : base(gameLog, view, world) { }

        public override void Update(FrameContext frameContext)
        {
            throw new NotImplementedException();
        }

        public void AddMessage(string message)
        {
            Model.AddMessage(message);
        }

        public void AddMessage(string message, Color4 color)
        {
            Model.AddMessage(message, color);
        }
    }
}
