using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nullable
{
    internal class Program
    {
        static void Main(string[] args)
        /* {
             int? age = null;
             Console.WriteLine(age);
         }*/



        {


            Nullable<int> age = 20;
            Console.WriteLine("age:" + age.Value);
        }
    }
}
