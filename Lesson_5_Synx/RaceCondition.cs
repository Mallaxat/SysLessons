using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace SynchronizationSandbox
{
    internal class RaceCondition
    {
        private static readonly object lockObj = new object();
        private static int account = 1500;


        private static bool WithdrawMoney(string clientID, int amount)
        {
            Thread.Sleep(10);
            int balance;
            bool success;

            lock (lockObj)
            {
                if (account >= amount)
                {
                    account -= amount;
                    balance = account;
                    success = true;
                }
                else
                {
                    balance = account;
                    success = false;
                }
            }
            if (success)
            {
                Console.WriteLine($"[{clientID}] Снято {amount} руб., остаток {balance}");
            }
            else
            {
                Console.WriteLine($"[{clientID}] Недостаточно денег, остаток {balance}");
            }

            return success;

        }

        public static void RunWithdraw(string clientID)
        {
            bool isEnd;
            do
            {
                isEnd = WithdrawMoney(clientID, 100); // снимаем по 100 руб пока не кончатся деньги
            } while (isEnd);
        }

        public static void RunWithdrawParallel()
        {
            Thread t1 = new Thread(() => RunWithdraw("t1"));
            Thread t2 = new Thread(() => RunWithdraw("t2"));

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();
        }
    }
}
