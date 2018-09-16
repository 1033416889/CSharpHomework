using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpHomeworkWeek2
{
    class Program
    {

        static void Main(string[] args)
        {
            System.Console.Write("Please input an integer(int32): ");
            int num;
            if (Int32.TryParse(Console.ReadLine(), out num))
            {
                for(int i = 2; i <= Math.Sqrt(num); i++)
                {
                    bool flag = false;
                    while (num % i == 0)
                    {
                        num /= i;
                        flag = true;
                    }
                    if(flag)
                    {
                        Console.Write($"{i} ");
                    }
                }
                if (num > 1)
                {
                    Console.Write(num);
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Input is invalid!");
            }
        }
    }
}
