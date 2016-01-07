using Explorer.Entities;
using Explorer.Infrastructure.Helpers;
using Explorer.Models;
using Explorer.Views;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Controllers
{
    public class PlayerController : BaseController<Player>
    {
        private EntityMoveHelper MoveHelper;
        private ILogController GameLog;

        public PlayerController(Player player, IView<Player> view, World world, EntityMoveHelper moveHelper, ILogController gameLog) : base(player, view, world)
        {
            MoveHelper = moveHelper;
            GameLog = gameLog;
        }

        public override void Update(FrameContext frameContext)
        {
            Model.AddEnergy();

            if (Model.IsReadyToAct())
            {
                var wasClear = MoveHelper.Move(Model, frameContext.LastPlayerAction);

                frameContext.LastPlayerAction = Infrastructure.Enums.MoveAction.Idle;

                if (!wasClear)
                {
                    GameLog.AddMessage("Ouch!", Color4.Red);
                }
                else if (Model.OldX != Model.X || Model.OldY != Model.Y)
                {
                    GameLog.AddMessage($"Player moved to x={Model.X}, y={Model.Y}");
                }
            }
        }
    }
}
