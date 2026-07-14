using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson_4_Matric
{
    //класс с процедурами матричных вычислений
    // MatrixCalculations - класс с процедурами матричных вычислений (в примере это суммирование матриц)
    internal static class MatrixCalculations
    {
        // Sum - поэлементное сложение двух матриц
        // вход: m1, m2 - матрицы для сложения
        // выход: сумма матриц m1, m2 в result (возврат через параметр-массив, память под который выделяется снаружи)
        public static void Sum(int[][] m1, int[][] m2, int[][] result)
        {
            for (int i = 0; i < m1.Length; i++)
            {
                for (int j = 0; j < m1[i].Length; j++)
                {
                    result[i][j] = m1[i][j] + m2[i][j];
                }
            }
        }

        // SumParallel - поэлементное сложение двух матриц с распараллеливанием на threadCount потоков
        // вход: m1, m2 - матрицы для сложения, threadCount - кол-во потоков, на которые надо распараллелить операцию сложения
        // выход: сумма матриц m1, m2 в result (возврат через параметр-массив, память под который выделяется снаружи)
        public static void SumParallel(int[][] m1, int[][] m2, int[][] result, int threadCount)
        {
            // получим размеры матрицы
            int m = m1.Length;      // кол-во строк
            int n = m1[0].Length;   // кол-во столбцов

            int rowsPerThread = m / threadCount; // кол-во строк на один поток
            // НО последний поток будет обрабатывать все оставшиеся строки - чтобы все было обработано

            // массив потоков, в рамках которых будут производиться вычисления для все матрицы
            Thread[] threads = new Thread[threadCount];
            for (int k = 0; k < threadCount; k++)
            {
                int start = k * rowsPerThread;
                int end = start + rowsPerThread;
                if (k == threadCount - 1)
                {
                    end = m; // последний поток обрабабтывает до последней строки
                }
                threads[k] = new Thread(() =>
                {
                    for (int i = start; i < end; i++)
                    {
                        for (int j = 0; j < m1[i].Length; j++)
                        {
                            result[i][j] = m1[i][j] + m2[i][j];
                        }
                    }
                });
                threads[k].Start();
            }

            // послед запуска всех потоков дождаться их завершения
            foreach (Thread t in threads)
            {
                t.Join();
            }
        }

        
        public static void Multiplication(int[][] m1, int[][] m2, int[][] result)
        {

            for (int i = 0; i < m1.Length; i++)
            {
                for (int j = 0; j < m2[0].Length; j++)
                {
                    int sum = 0;

                    for (int k = 0; k < m1[0].Length; k++)
                    {
                        sum += m1[i][k] * m2[k][j];
                    }

                    result[i][j] = sum;
                }
            }

        }

        public static void MultiplicationParallel(int[][] m1, int[][] m2, int[][] result, int threadCount)
        {
            int m = m1.Length;      // кол-во строк
            int n = m1[0].Length;   // кол-во столбцов

            int rowsPerThread = m / threadCount; // кол-во строк на один поток
            Thread[] threads = new Thread[threadCount];


            for(int t=0; t < threadCount; t++)
            {
                int start = t * rowsPerThread;
                int end = start + rowsPerThread;
                if (t == threadCount - 1)
                {
                    end = m; // последний поток обрабабтывает до последней строки
                }
                threads[t] = new Thread(() =>
                {
                    for (int i = start; i < end; i++)
                    {
                        for (int j = 0; j < m2[0].Length; j++)
                        {
                            int sum = 0;

                            for (int k = 0; k < m1[0].Length; k++)
                            {
                                sum += m1[i][k] * m2[k][j];
                            }

                            result[i][j] = sum;
                        }
                    }
                });
                threads[t].Start();
                // послед запуска всех потоков дождаться их завершения

            }
            foreach (Thread t in threads)
            {
                t.Join();
            }

        }

    }
}
