using System.Diagnostics.Metrics;

namespace Lesson_2_Thread
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //внутри нужно указать какой метод выполнять
            //Метод

            /*            Thread thread = new Thread(SayGreeing);
                        Console.WriteLine("Thread created");
                        thread.Start();
                        Console.WriteLine("Thread start");*/
            //в 1 потоке
            /*      mCounter("no-delay",10,0);
                  mCounter("short-delay", 10, 0);
                  mCounter("long-delay", 10, 0);*/

            /*            Console.Clear();
                        //2 в нескольких потоках
                        //Чтобы запустить функцию с параметрами нужно:
                        //параметр типо object и записывать его до запуска потока
                        //либо обернуть в лямбду
                        //создать
                        Thread t1=new Thread(()=> mCounter("t1",10,1000));
                        Thread t2 = new Thread(() => mCounter("t2", 5, 1000));
                        Console.WriteLine("Threads create");

                        //запустить
                        t1.Start();
                        t2.Start();
                        Console.WriteLine("Threads started");

                        //дождаться завершения
                        t1.Join();  //блокирует другие потоки, пока не завершится T1
                        t2.Join();  //Main блокируется, пока не завершится t2
                        Console.WriteLine("Threads stop");
            */

            //Утро
            Console.WriteLine("Однопоточный");
            MorningActivities.SingleThreadMorning();
            Console.WriteLine();
            Console.WriteLine("Многопоточный");
            MorningActivities.MultiThreadMorning();

        }

        public static void SayGreeing()
        {
            Console.WriteLine("Hello");
        }
        public static void mCounter(string name,int n,int delay)
        {
            Console.WriteLine($"[{name}] Start Counter");
            for(int i=0;i<n;i++)
            {
                Console.WriteLine($"[{name}] {i+1}");
                Thread.Sleep(delay);
            }
            Console.WriteLine($"[{name}] Finish Counter");
        }


    }
}
