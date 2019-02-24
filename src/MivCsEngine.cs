using System;
using System.Collections.Generic;
using System.IO;
using mivcs.Rendering;

namespace mivcs
{
    public class MivCsEngine : IEngine
    {

        public List<DisplayBuffer> Buffers { get; set; } = new List<DisplayBuffer>()
        {
            new DisplayBuffer(80,40)
        };

        public FileBuffer FileBuffer { get; set; } = new FileBuffer();

        public int SelectedBufferIndex { get; set; }
        public DisplayBuffer SelectedBuffer => Buffers[SelectedBufferIndex];

        public MivCsEngine()
        {
           
        }
        
        public void Resize(int width, int height)
        {
           SelectedBuffer.Resize(width, height);
           
        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            FileBuffer.HandleKey(info);
        }

        public void Redraw()
        {
            FileBuffer.Draw(SelectedBuffer);
            SelectedBuffer.SwapBuffer();
           
        }
    }
}