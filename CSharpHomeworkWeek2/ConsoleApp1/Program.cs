using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<int> list = new List<int>();
            //Console.WriteLine("Please input an array(input an EOF to stop input) :");
            ////EOF = ctrl + z
            //int num;
            //while (Int32.TryParse(Console.ReadLine(),out num))
            //{
            //    list.Add(num);
            //}
            int[] A = { 134, 1234, 123, 3457, 782, 346, 237, 4745, 156, 74, 12346, 345, 2, 7, 78 };
            int max = 0, min = 0x7fffffff;
            Int64 sum = 0;
            foreach(int x in A)
            {
                if(x>max)
                {
                    max = x;
                }
                if (x < min)
                {
                    min = x;
                }
                sum += x;
            }
            Console.WriteLine($"Max: {max} ,Min: {min} ,Average: {sum / A.Length} ,Sum: {sum}");
        }
    }

    
    class Vector
    {

    }
}
