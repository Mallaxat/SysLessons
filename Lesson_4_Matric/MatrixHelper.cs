using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson_4_Matric
{
    // MatrixHelper - класс, с различными вспомогательными процедурами для работы с матрицами
    internal static class MatrixHelper
    {
        // CreateEmpty - метод создания матрицы m x n, заполненной нулями
        public static int[][] CreateEmpty(int m, int n)
        {
            int[][] matrix = new int[m][];
            for (int i = 0; i < m; i++)
            {
                matrix[i] = new int[n];
            }
            return matrix;
        }

        // GenerateRandom - генерация матрицы m x n, заполненной случайными значениями в диапазоне от min до max
        public static int[][] GenerateRandom(int m, int n, int min, int max, Random random)
        {
            int[][] matrix = CreateEmpty(m, n);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i][j] = random.Next(min, max + 1);
                }
            }
            return matrix;
        }


        // Print - метод вывода матрицы на экран с сообщением и разделителем
        public static void Print(int[][] matrix, string message, string separator = "========================")
        {
            Console.WriteLine(message);
            foreach (int[] row in matrix)
            {
                foreach (int item in row)
                {
                    Console.Write($"{item} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(separator);
        }

        // AreEqual - сравнение двух матриц поэлементно
        public static bool AreEqual(int[][] m1, int[][] m2)
        {
            for (int i = 0; i < m1.Length; i++)
            {
                for (int j = 0; j < m1[i].Length; j++)
                {
                    if (m1[i][j] != m2[i][j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
