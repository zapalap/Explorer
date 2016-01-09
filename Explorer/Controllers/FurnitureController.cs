using Explorer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Models;
using Explorer.Views;
using Explorer.Infrastructure.Helpers;
using Explorer.Infrastructure.Fov;

namespace Explorer.Controllers
{
    public class FurnitureController : BaseController<Furniture>
    {
        private ILogController GameLog;

        public FurnitureController(Furniture furniture, IView<Furniture> view, World world, ILogController gameLog) : base(furniture, view, world)
        {
            GameLog = gameLog;
        }

        public override void Draw()
        {
            Model.Visible = World.Map.Tiles[Model.X, Model.Y].Visible;
            View.Draw(Model);
        }

        public override void Update(FrameContext frameContext)
        {
            return;
        }
    }
}
