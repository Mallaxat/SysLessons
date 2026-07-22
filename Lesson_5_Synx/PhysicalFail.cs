using System;
using System.Collections.Generic;
using System.Text;

namespace SynchronizationSandbox
{
    internal class PhysicalFail
    {
        public static void RunExampleWithFail()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            dictionary["count"] = 0;

            Thread[] threads = new Thread[10];
            for (int k = 0; k < threads.Length; k++)
            {
                threads[k] = new Thread(() =>
                {
                    for (int i = 0; i < 100_000; i++)
                    {
                        dictionary["count"]++;
                        if (dictionary["count"] % 100 == 0)
                        {
                            dictionary[DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString()] = dictionary["count"];
                        }
                    }
                });
                threads[k].Start();
            }

            foreach (Thread t in threads)
            {
                t.Join();
            }

            Console.WriteLine("count = " + dictionary["count"]); // приведет ли это к проблеме?
            foreach (KeyValuePair<string, int> p in dictionary)
            {
                Console.WriteLine(p);
            }
        }

        public static void RunExampleSuccess()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            dictionary["count"] = 0;

            object lockObject = new object(); // объект-блокировки - некоторый объект в куче

            Thread[] threads = new Thread[10];
            for (int k = 0; k < threads.Length; k++)
            {
                threads[k] = new Thread(() =>
                {
                    for (int i = 0; i < 100_000; i++)
                    {
                        lock (lockObject)
                        {
                            // код критической секции
                            dictionary["count"]++;
                            if (dictionary["count"] % 100 == 0)
                            {
                                dictionary[DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString()] = dictionary["count"];
                            }
                        }
                    }
                });
                threads[k].Start();
            }

            foreach (Thread t in threads)
            {
                t.Join();
            }

            Console.WriteLine("count = " + dictionary["count"]); // приведет ли это к проблеме?
            foreach (KeyValuePair<string, int> p in dictionary)
            {
                Console.WriteLine(p);
            }
        }
    }
}
