using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CSharpHomeworkProject1
{
    public delegate void ClockHander(object sender,ClockEventArgs args );

    public class ClockEventArgs : EventArgs
    {
        public DateTime Time { get; set; }
    }

    public class MyClock
    {
        public event ClockHander OnAlarm;

        public void Alarm(DateTime time)
        {
            ClockEventArgs args = new ClockEventArgs
            {
                Time = time
            };
            OnAlarm(this, args);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入闹钟时间(格式:HH:MM):");
            string inputTime = Console.ReadLine();
            try
            {
                DateTime alarmTime = DateTime.Parse(inputTime);
                DateTime now = DateTime.Now;
                if (alarmTime < now)
                {
                    alarmTime += TimeSpan.Parse("1.00:00:00");
                }
                TimeSpan span = alarmTime - now;
                Console.WriteLine($"闹钟将会在{span.Hours}小时{span.Minutes}分钟后提醒！");

                while (!isOnTime(alarmTime))
                {
                    Thread.Sleep(500);
                }
                
            }
            catch
            {
                Console.WriteLine("输入异常！");

            }
        }

        public static bool isOnTime(DateTime alarmTime)
        {
            if (Math.Abs((alarmTime - DateTime.Now).Seconds) <= 1)
            {
                MyClock alarm = new MyClock();

                alarm.OnAlarm += Alarming;

                alarm.Alarm(alarmTime);
                return true;
            }
            else
            {
                return false;
            }
        }

        static void Alarming(object sender, ClockEventArgs args)
        {
            Console.WriteLine("时间到！");
        }
    }
}
