using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace mivcs
{
    public static class ConsoleUtils
    {
        private const int STD_OUTPUT_HANDLE = -11;
        private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;
        private const uint DISABLE_NEWLINE_AUTO_RETURN = 0x0008;

        [DllImport("kernel32.dll")]
        private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();
        
        public static void InitConsole()
        {
            bool isWindows = System.Runtime.InteropServices.RuntimeInformation
                .IsOSPlatform(OSPlatform.Windows);

            if (isWindows)
            {
                var iStdOut = GetStdHandle(STD_OUTPUT_HANDLE);
                if (!GetConsoleMode(iStdOut, out uint outConsoleMode))
                {
                    Console.WriteLine("failed to get output console mode");
                    Console.ReadKey();
                    return;
                }

                outConsoleMode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING | DISABLE_NEWLINE_AUTO_RETURN;
                if (!SetConsoleMode(iStdOut, outConsoleMode))
                {
                    Console.WriteLine($"failed to set output console mode, error code: {GetLastError()}");
                    Console.ReadKey();
                    return;
                }
            }
            
            //this makes the codes posible, that dont work unless you do this
            var stdout = Console.OpenStandardOutput();
            var con = new StreamWriter(stdout, Encoding.ASCII);
            con.AutoFlush = true;
            Console.SetOut(con);
            
            Console.Clear();
        }
    }
}