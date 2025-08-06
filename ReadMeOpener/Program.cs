using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadMeOpener
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Process.Start("notepad.exe", "Beni Oku.txt");
        }
    }
}
