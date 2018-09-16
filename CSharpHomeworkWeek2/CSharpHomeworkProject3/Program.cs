using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpHomeworkProject3
{
    class Program
    {
        static void Main(string[] args)
        {
            bool[] isPrime = new bool[105];
            List<int> primes = new List<int>();
            for(int i =  0; i <= 100; i++)
            {
                isPrime[i] = true;
            }
            for(int i = 2; i <= 100; i+=2)
            {
                if (isPrime[i])
                {
                    primes.Add(i);
                    for(int j = i * 2; j <= 100; j += i)
                    {
                        isPrime[j] = false;
                    }
                }
                if (i == 2) i = 1;
            }

            for(int i = 0; i < primes.Count; i++)
            {
                Console.Write($"{primes[i]} ");
            }
            Console.WriteLine();
        }
    }
}
