using Explorer.Controllers;
using Explorer.Entities;
using Explorer.Infrastructure.Enums;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Infrastructure.Helpers
{
    public class EntityMoveHelper : IMoveHelper
    {
        private World World;
        private ILogController GameLog;

        public EntityMoveHelper(World world, ILogController gameLog)
        {
            World = world;
            GameLog = gameLog;
        }

        public bool Move(BaseEntity entity, MoveAction action)
        {
            entity.OldY = entity.Y;
            entity.OldX = entity.X;

            switch (action)
            {
                case Infrastructure.Enums.MoveAction.Idle:
                    break;
                case Infrastructure.Enums.MoveAction.MoveUp:
                    entity.Y -= 1;
                    break;
                case Infrastructure.Enums.MoveAction.MoveDown:
                    entity.Y += 1;
                    break;
                case Infrastructure.Enums.MoveAction.MoveRight:
                    entity.X += 1;
                    break;
                case Infrastructure.Enums.MoveAction.MoveLeft:
                    entity.X -= 1;
                    break;
                default: return false;
            }

            if (!IsLegal(entity.X, entity.Y))
            {
                entity.X = entity.OldX;
                entity.Y = entity.OldY;
                return false;
            }

            return true;
        }

        private bool IsLegal(int x, int y)
        {
            if ((x < 0 || y < 0) || (x > World.Map.Tiles.GetLength(0)-1 || y > World.Map.Tiles.GetLength(1)-1))
            {
                return false;
            }

            if (World.Map.Tiles[x,y].Furniture.Blocking)
            {
                return false;
            }

            return true;
        }
    }
}
