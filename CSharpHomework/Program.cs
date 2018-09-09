using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpHomework
{
    class Program
    {
        static void Main(string[] args)
        {
            int a, b;
            Console.WriteLine("Please input 2 integers: ");
            a = int.Parse(Console.ReadLine()); 
            b = int.Parse(Console.ReadLine());
            Console.WriteLine(a + " * " + b + " = " + a * b);
        }
    }
}
