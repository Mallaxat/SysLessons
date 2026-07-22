using System;
using System.Collections.Generic;
using System.Text;

namespace SynchronizationSandbox
{
    // LogicalFail - пример с нарушение логической целостности данных в следствие гонок данных
    internal class LogicalFail
    {
        public static void RunExampleWithFail()
        {
            int count = 0;
            Thread[] threads = new Thread[10];
            for (int k = 0; k < threads.Length; k++)
            {
                threads[k] = new Thread(() => {
                    for (int i = 0; i < 100_000; i++)
                    {
                        count++; // причина в не атомарности инкремента
                        // решение - сделать инкремент атомарным
                    }
                });
                threads[k].Start();
            }

            foreach (Thread t in  threads)
            {
                t.Join();
            }

            Console.WriteLine("count = " + count); // ожидаем 1000_000 но получим рандом
        }

        public static void RunExampleSuccess()
        {
            int count = 0;
            Thread[] threads = new Thread[10];
            for (int k = 0; k < threads.Length; k++)
            {
                threads[k] = new Thread(() => {
                    for (int i = 0; i < 100_000; i++)
                    {
                        // Interloced - класс, предоставляющий процедуры примтивных атомарных операций
                        Interlocked.Increment(ref count);
                    }
                });
                threads[k].Start();
            }

            foreach (Thread t in threads)
            {
                t.Join();
            }

            Console.WriteLine("count = " + count); // ожидаем 1000_000 но получим рандом
        }

    }
}
