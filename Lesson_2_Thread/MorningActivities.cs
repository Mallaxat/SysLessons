using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Lesson_2_Thread
{
    //класс с процедурами игрушечного примера различных утренних дел
    internal class MorningActivities
    {
        static void TakeShower()
        {
            Console.WriteLine("TakeShower start");
            Thread.Sleep(10000);
            Console.WriteLine("TakeShower stop");
        }

        //Согреть чайник
        static void WarmKettle()
        {
            Console.WriteLine("TWarmKettle start");
            Thread.Sleep(3000);
            Console.WriteLine("WarmKettle stop");
        }


        static void HaveBreakfast()
        {
            Console.WriteLine("HaveBreakfast start");
            Thread.Sleep(7000);
            Console.WriteLine("HaveBreakfast stop");
        }

        //Процедуры выполнения дел
        public static void SingleThreadMorning()
        {
            Stopwatch sw = Stopwatch.StartNew();

            TakeShower();
            WarmKettle();
            HaveBreakfast();

            Console.WriteLine($"Complete Single {sw.ElapsedMilliseconds} ms");
        }
        public static void MultiThreadMorning()
        {
            Stopwatch sw = Stopwatch.StartNew();

            Thread t1 = new Thread(WarmKettle);
            Thread t2 = new Thread(TakeShower);
            t1.Start();
            t2.Start();

            t2.Join();

            HaveBreakfast();

            Console.WriteLine($"Complete Multi {sw.ElapsedMilliseconds} ms");
        }

    }
}
