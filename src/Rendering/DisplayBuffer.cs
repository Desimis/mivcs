using System;
using System.Drawing;

namespace mivcs.Rendering
{
    public class DisplayBuffer
    {
        public static readonly Color Background = Color.Teal;
        public static readonly Color Foreground = Color.White;

        public bool Invalid { get; set; } = true;

        public DisplayBuffer(int width, int height)
        {
            Width = width;
            Height = height;

            _bufferA = new DisplayCell[width * height];

            Array.Fill(_bufferA, ' ');

            _bufferB = new DisplayCell[width * height];

            Array.Fill(_bufferB, ' ');
        }

        public void Resize(int width, int height)
        {
            Width = width;
            Height = height;

            _bufferA = new DisplayCell[width * height];

            Array.Fill(_bufferA, ' ');

            _bufferB = new DisplayCell[width * height];

            Array.Fill(_bufferB, ' ');
        }

        private DisplayCell[] _bufferA;
        private DisplayCell[] _bufferB;

        public int Width { get; set; }
        public int Height { get; set; }


        public DisplayCell this[int x, int y]
        {
            get => _bufferA[x + (y * Width)];
            set => _bufferA[x + (y * Width)] = value;
        }

        public void SwapBuffer(bool forceRefresh = false)
        {
            SwapBuffer(0, 0, Console.BufferWidth, Console.BufferHeight, forceRefresh);
        }

        public void SwapBuffer(int x, int y, int width, int height, bool forceRefresh = false)
        {
            Console.CursorVisible = false;

            for (int w = x; w < Math.Max(Width, width); w++)
            {
                for (int h = y; h < Math.Min(Height, height); h++)
                {
                    var oX = w - x;
                    var oY = h - y;

                    var cell = _bufferA[oX + (oY * Width)];
                    var cellb = _bufferB[oX + (oY * Width)];

                    if (cell.Char != cellb.Char || forceRefresh || Invalid)
                    {
                        Console.SetCursorPosition(w, h);

                        ForegroundColor(cell.Foreground);
                        BackgroundColor(cell.Background);
                        Console.Write(cell.Char);
                    }
                }
            }

            Array.Copy(_bufferA, _bufferB, _bufferA.Length);

            Invalid = false;
            
            ResetColor();
        }

        private void ForegroundColor(Color c)
        {
            Console.Write($"\x1b[38;2;{c.R};{c.G};{c.B}m");
        }

        private void BackgroundColor(Color c)
        {
            Console.Write($"\x1b[48;2;{c.R};{c.G};{c.B}m");
        }

        private void ResetColor()
        {
            Console.Write($"\x1b[0m");
        }
    }
}