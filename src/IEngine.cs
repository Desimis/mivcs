using System;

namespace mivcs
{
    public interface IEngine
    {
        void Resize(int width, int height);
        void HandleKey(ConsoleKeyInfo info);
        void Redraw();
    }
}