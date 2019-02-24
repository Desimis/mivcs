using System;
using mivcs.Rendering;

namespace mivcs
{
    public class FileBuffer
    {
        public string Content { get; set; } = "";

        private int leng = 0;

        public FileBuffer()
        {
            leng = Content.Length;
        }
        
        public void HandleKey(ConsoleKeyInfo key)
        {
            var c = key.KeyChar;


            if ((byte) c >= 32 && (byte) c <= 126)
            {
                if (key.Modifiers == ConsoleModifiers.Shift)
                {
                    c = c.ToString().ToUpper()[0];
                }

                Content += c;
            }
            else
            {
                switch (key.Key)
                {
                    case ConsoleKey.Backspace:
                        Content = Content.Remove(Content.Length - 1);
                        break;
                }
            }
        }

        public void Draw(DisplayBuffer db)
        {
            if (leng > Content.Length)
            {
                db.Invalid = true;
            }
            
            
            var x = 0;
            var y = 0;
            
            for (var i = 0; i < Content.Length; i++)
            {
                var c = Content[i];
                if (x > db.Width)
                {
                    x = 0;
                    y++;
                }

                db[x, y] = c;
                
                x++;
            }
            
            leng = Content.Length;
        }
    }
}