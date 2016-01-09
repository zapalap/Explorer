using Explorer.Models;

namespace Explorer.Input
{
    public interface IInputHandler
    {
        bool Handle(FrameContext frameContext);
    }
}