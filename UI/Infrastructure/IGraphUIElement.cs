using System.Windows;

namespace UI.Infrastructure
{
    public interface IGraphUIElement
    {
        Point TopLeft { get; }
        Point BottomRight { get; }

        void ChangeCanvasCenter(Vector difference);
    }
}