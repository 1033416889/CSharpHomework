using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpHomeworkProject2
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderService myService = new OrderService();
            myService.StartOrderService();
        }
    }
}
