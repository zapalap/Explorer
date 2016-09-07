using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Models;
using Explorer.Views;
using Explorer.Entities;
using Explorer.Infrastructure.Fov;

namespace Explorer.Controllers
{
    public abstract class BaseController<T> : IController where T : BaseEntity
    {
        protected T Model;
        protected IView<T> View;
        protected World World;

        public BaseController(T model, IView<T> view, World world)
        {
            Model = model;
            View = view;
            World = world;
        }

        public virtual void Draw()
        {
            View.Draw(Model);
        }

        public abstract void Update(FrameContext frameContext);
    }
}
