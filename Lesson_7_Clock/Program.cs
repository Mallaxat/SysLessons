using Microsoft.Win32;
using System.Diagnostics;
using System.Globalization;

namespace Lesson_7_Clock
{
    internal class Program
    {
        private static void Main()
        {
            string processName = Process.GetCurrentProcess().ProcessName;
         
            using RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(processName);

            object lastStartTime = registryKey.GetValue("LastStartTime");

            if (lastStartTime == null)
            {
                Console.WriteLine( "Программа ранее не запускалась на этом устройстве");
            }
            else
            {
                Console.WriteLine($"Время последнего запуска: {lastStartTime}");
            }

            registryKey.SetValue(
                "LastStartTime",
                DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));

            Console.WriteLine("Нажмите Ctrl + C, чтобы завершить процесс");

            while (true)
            {
                Console.Write( $"\rТекущее время: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                Thread.Sleep(1000);
            }
        }
    }
}
