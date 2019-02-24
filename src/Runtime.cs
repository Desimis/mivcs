using System;
using System.Diagnostics;
using System.Threading;

namespace mivcs
{
    public class Runtime
    {
        public Action<int, int> ResizeEvent { get; set; }
        public Action<ConsoleKeyInfo> KeyEvent { get; set; }
        public Action Redraw { get; set; }

        private int _lastWidth;
        private int _lastHeight;

        public Runtime Hook(IEngine e)
        {
            Redraw = e.Redraw;
            KeyEvent = e.HandleKey;
            ResizeEvent = e.Resize;

            return this;
        }


        public Runtime()
        {
            ConsoleUtils.InitConsole();
            Console.SetWindowSize(80, 40);
            Console.SetBufferSize(80, 40);

            _lastWidth = Console.BufferWidth;
            _lastHeight = Console.BufferHeight;
        }

        public void Start()
        {
            _lastWidth = Console.BufferWidth;
            _lastHeight = Console.BufferHeight;

            ResizeEvent(_lastWidth, _lastHeight);
            Redraw();

            while (true)
            {
                Debug.WriteLine(Console.BufferHeight);
                
                if (_lastWidth != Console.BufferWidth && 
                    _lastHeight != Console.BufferHeight)
                {
                    _lastWidth = Console.BufferWidth;
                    _lastHeight = Console.BufferHeight;

                    ResizeEvent(_lastWidth, _lastHeight);
                    Redraw();
                }


                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    KeyEvent(key);
                    Redraw();
                }
            }
        }
    }
}