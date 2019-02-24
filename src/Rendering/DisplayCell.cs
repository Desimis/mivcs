using System.Drawing;

namespace mivcs.Rendering
{
    public class DisplayCell
    {
        public Color Background { get; set; }
        public Color Foreground { get; set; }
        public char Char { get; set; }
        
        public static implicit operator DisplayCell(char value)
        {
            return new DisplayCell
            {
                Char = value,
                Background = DisplayBuffer.Background,
                Foreground = DisplayBuffer.Foreground,
            };
        }
    }
}