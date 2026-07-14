using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace newthread
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(printnumbers);
            thread.Start();
           
        }
        static void printnumbers()
        {
            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(1000);
            }
        }
    }
}
