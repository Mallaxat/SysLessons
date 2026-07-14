using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace Lesson_4_Matric
{
    //Для тестирования алгоритмов
    // MatrixCalculationsTest - класс для тестирования алгоритмов матричных вычислений
    internal static class MatrixCalculationsTest
    {
        // TestSum - тестирование однопоточного суммирования
        public static void TestSum()
        {
            // 1. ввод исходных данных
            Console.Write("Enter m: ");
            int m = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter n: ");
            int n = Convert.ToInt32(Console.ReadLine());
            int min = 0, max = 4;

            // 2. подготовка матриц
            Random random = new Random();
            int[][] m1 = MatrixHelper.GenerateRandom(m, n, min, max, random);
            int[][] m2 = MatrixHelper.GenerateRandom(m, n, min, max, random);
            int[][] result = MatrixHelper.CreateEmpty(m, n);
            Console.WriteLine("m1, m2 generated; result created");

            // 3. выполнение операции
            Stopwatch sw = Stopwatch.StartNew();
            MatrixCalculations.Sum(m1, m2, result);
            sw.Stop();

            // 4. вывод результата
            Console.WriteLine($"Sum completed, elapsed {sw.ElapsedMilliseconds} ms.");
            Console.Write("Do you want to print matrices? (y / n)");
            char reply = Console.ReadKey(true).KeyChar;
            Console.WriteLine();
            if ("yYнН".Contains(reply))
            {
                MatrixHelper.Print(m1, "m1:");
                MatrixHelper.Print(m2, "m2:");
            }
        }
        public static void TestMult()
        {
            // 1. ввод исходных данных
            Console.Write("Enter m: ");
            int m = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter n: ");
            int n = Convert.ToInt32(Console.ReadLine());
            int min = 0, max = 4;

            // 2. подготовка матриц
            Random random = new Random();
            int[][] m1 = MatrixHelper.GenerateRandom(m, n, min, max, random);
            int[][] m2 = MatrixHelper.GenerateRandom(m, n, min, max, random);
            int[][] result = MatrixHelper.CreateEmpty(m, n);
            Console.WriteLine("m1, m2 generated; result created");

            // 3. выполнение операции
            Stopwatch sw = Stopwatch.StartNew();
            MatrixCalculations.Multiplication(m1, m2, result);
            sw.Stop();

            // 4. вывод результата
            Console.WriteLine($"Sum completed, elapsed {sw.ElapsedMilliseconds} ms.");
            Console.Write("Do you want to print matrices? (y / n)");
            char reply = Console.ReadKey(true).KeyChar;
            Console.WriteLine();
            if ("yYнН".Contains(reply))
            {
                MatrixHelper.Print(m1, "m1:");
                MatrixHelper.Print(m2, "m2:");
            }
        }
        public static void TestMultParallel()
        {
            // 1. ввод исходных данных
            Console.Write("Enter m: ");
            int m = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter n: ");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter k: ");
            int k = Convert.ToInt32(Console.ReadLine());
            int min = 0, max = 4;

            // 2. подготовка матриц
            Random random = new Random();
            int[][] m1 = MatrixHelper.GenerateRandom(m, n, min, max, random);
            int[][] m2 = MatrixHelper.GenerateRandom(m, n, min, max, random);
            int[][] result = MatrixHelper.CreateEmpty(m, n);
            Console.WriteLine("m1, m2 generated; result created");

            // 3. выполнение операции
            Stopwatch sw = Stopwatch.StartNew();
            MatrixCalculations.MultiplicationParallel(m1, m2, result, k);
            sw.Stop();

            // 4. вывод результата
            Console.WriteLine($"SumParallel completed, elapsed {sw.ElapsedMilliseconds} ms.");
            Console.Write("Do you want to print matrices? (y / n)");
            char reply = Console.ReadKey(true).KeyChar;
            Console.WriteLine();
            if ("yYнН".Contains(reply))
            {
                MatrixHelper.Print(m1, "m1:");
                MatrixHelper.Print(m2, "m2:");
                MatrixHelper.Print(result, "result:");
            }
        }


        // TestSumParallel - тестирование многопоточного суммирования
        public static void TestSumParallel()
        {
            // 1. ввод исходных данных
            Console.Write("Enter m: ");
            int m = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter n: ");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter k: ");
            int k = Convert.ToInt32(Console.ReadLine());
            int min = 0, max = 4;

            // 2. подготовка матриц
            Random random = new Random();
            int[][] m1 = MatrixHelper.GenerateRandom(m, n, min, max, random);
            int[][] m2 = MatrixHelper.GenerateRandom(m, n, min, max, random);
            int[][] result = MatrixHelper.CreateEmpty(m, n);
            Console.WriteLine("m1, m2 generated; result created");

            // 3. выполнение операции
            Stopwatch sw = Stopwatch.StartNew();
            MatrixCalculations.SumParallel(m1, m2, result, k);
            sw.Stop();

            // 4. вывод результата
            Console.WriteLine($"SumParallel completed, elapsed {sw.ElapsedMilliseconds} ms.");
            Console.Write("Do you want to print matrices? (y / n)");
            char reply = Console.ReadKey(true).KeyChar;
            Console.WriteLine();
            if ("yYнН".Contains(reply))
            {
                MatrixHelper.Print(m1, "m1:");
                MatrixHelper.Print(m2, "m2:");
                MatrixHelper.Print(result, "result:");
            }
        }

        // TestSums - сравнение результатов однопоточного и многопоточного алгоритмов
        public static void TestSums()
        {
            // 1. ввод исходных данных
            Console.Write("Enter m: ");
            int m = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter n: ");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter k: ");
            int k = Convert.ToInt32(Console.ReadLine());
            int min = 0, max = 4;

            // 2. подготовка матриц
            Random random = new Random();
            int[][] m1 = MatrixHelper.GenerateRandom(m, n, min, max, random);
            int[][] m2 = MatrixHelper.GenerateRandom(m, n, min, max, random);
            int[][] result = MatrixHelper.CreateEmpty(m, n);
            int[][] resultParallel = MatrixHelper.CreateEmpty(m, n);
            Console.WriteLine("m1, m2 generated; result, resultParallel created");

            // 3. выполнение операции
            MatrixCalculations.Sum(m1, m2, result);
            MatrixCalculations.SumParallel(m1, m2, resultParallel, k);

            // 4. вывод результата
            if (MatrixHelper.AreEqual(result, resultParallel))
            {
                Console.WriteLine("Equal!");
            }
            else
            {
                Console.WriteLine("Not equal :c");
            }
        }
    }
}
