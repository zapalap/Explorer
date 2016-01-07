using Explorer.Entities;
using Explorer.Infrastructure.Enums;
using Explorer.Infrastructure.Helpers;
using Explorer.Models;
using Explorer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Controllers
{
    public class MonsterController : BaseController<Creature>
    {
        private EntityMoveHelper MoveHelper;
        private Random Random;
        private ILogController GameLog;

        public MonsterController(Creature monster, IView<Creature> view, World world, EntityMoveHelper moveHelper, ILogController gameLog) : base(monster, view, world)
        {
            MoveHelper = moveHelper;
            GameLog = gameLog;
            Random = new Random();
        }

        public override void Update(FrameContext frameContext)
        {
            Model.AddEnergy();

            if (Model.IsReadyToAct())
            {
                var direction = Random.Next(0, 100);

                if (direction < 20)
                {
                    MoveHelper.Move(Model, MoveAction.MoveUp);
                }

                if (direction > 20 && direction < 40)
                {
                    MoveHelper.Move(Model, MoveAction.MoveDown);
                }

                if (direction > 40 && direction < 60)
                {
                    MoveHelper.Move(Model, MoveAction.MoveLeft);
                }

                if (direction > 60)
                {
                    MoveHelper.Move(Model, MoveAction.MoveRight);
                }
            }
        }
    }
}
