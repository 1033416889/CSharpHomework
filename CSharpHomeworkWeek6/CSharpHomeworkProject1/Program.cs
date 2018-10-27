using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace CSharpHomeworkProject2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Process[] process = Process.GetProcessesByName("chrome");
            //var pp = process.FirstOrDefault();
            //foreach (Process p in process)
            //{
            //    Console.WriteLine(p.ProcessName.ToString() + "   "+ p.MainWindowHandle.ToInt32().ToString()+"   "+p.Id.ToString());
            //}
            //const int WM_KEYDOWN = 0x0100;
            //const int WM_KEYUP = 0x0101;
            //IntPtr ppp = (IntPtr)404;
            //SendMessage(ppp, WM_KEYDOWN, 87, 0);
            //SendMessage(ppp, WM_KEYUP, 87, 0);
           
            OrderService myService = new OrderService();
            myService.StartOrderService();
        }
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
    }
}
