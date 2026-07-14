using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringConcanection._03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fristname = "HK_";
            string lastname = "Ahir";
            string name = string.Concat(fristname,lastname);
            Console.WriteLine(name);
        }
    }
}
