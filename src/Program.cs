using System;
using System.Drawing;

namespace mivcs
{
    class Program
    {
        static void Main(string[] args)
        {
          new Runtime().Hook(new MivCsEngine()).Start();
        }
    }
}