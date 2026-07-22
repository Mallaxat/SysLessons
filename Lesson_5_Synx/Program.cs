using SynchronizationSandbox;

namespace Lesson_5_Synx
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Скачать проект с примерами, открыть в Visual Studio, запустить (хотя как нибудь)
            // 2. Посмотреть примеры в классах LogicalFail и PhysicalFail, при необходимости запустить процедуры / задать вопросы
            // 3. Написать вызов RaceCondition.RunWithdrawParallel(); в Program.cs, запустить, убедиться что есть проблемы гонок данных и состояния гонки
            // 4. Попробовать пофиксить эти проблемы, внедрив синхронизацию в процедуру WithdrawMoney, можно использовать Interlocked / lock, проверить что работает
            // 5. Скинуть пропатченную версию RunWithdrawParallel в чат в виде скрина

            RaceCondition.RunWithdraw("g1");
        }
    }
}
