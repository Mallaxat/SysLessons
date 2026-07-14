namespace Lesson_4_Matric
{
    internal class Program
    {
        static void Main(string[] args)
        {

            /*
                            Console.WriteLine("Однопоточный режим");
                            MatrixCalculationsTest.TestSum();

                            Console.WriteLine("Многопоточный режим");
                            MatrixCalculationsTest.TestSumParallel();

            */

            Console.WriteLine("1 поток");
            MatrixCalculationsTest.TestMult();
            Console.WriteLine();

            Console.WriteLine("Многопоточность");
            MatrixCalculationsTest.TestMultParallel();
        }
    }
}
