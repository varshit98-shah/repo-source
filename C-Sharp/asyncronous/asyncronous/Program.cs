using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asyncronous
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("program start");

            await Task.Delay(4000);
            Console.WriteLine("program ended");
        }
    }
}
