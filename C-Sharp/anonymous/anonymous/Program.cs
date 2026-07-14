using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anonymous
{
    delegate void MyDelegate();
    internal class Program
    {
        static void Main(string[] args)
        {
            MyDelegate del = delegate ()
            {
                Console.WriteLine("Hello");
            };
            del();
        }
    }
}
