using Explorer.Models;

namespace Explorer.Controllers
{
    public interface IController
    {
        void Draw();
        void Update(FrameContext frameContext);
    }
}