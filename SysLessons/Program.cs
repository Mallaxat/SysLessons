
using System;
using System.Diagnostics;




//Задание 1
/*Вывести на экран помимо айди и процесса имени от 3 до 5 свойств процесса*/

//Дескриптор-По сути, это «паспорт» процесса: в нём собрана вся ключевая информация,
//необходимая ядру ОС для управления им на протяжении всего жизненного цикла.
//Класс который отвечает за работу с процессами
/*Process[] processes = Process.GetProcesses();

    foreach (Process process in processes)
    {
        Console.WriteLine($"Возвращает базовый приоритет связанного процесса {process.BasePriority}");
        Console.WriteLine($"Возвращает количество дескрипторов, открытых процессом {process.HandleCount}");
        Console.WriteLine($"Возвращает максимальный объем физической памяти в байтах{process.PeakWorkingSet64}");
        Console.WriteLine($"Возвращает набор потоков, выполняемых в связанном процессе {process.Threads.Count}");
        Console.WriteLine($"Получает объем страницной памяти в байтах, выделенный для связанного процесса " +
            $"{process.PagedMemorySize64}");
    }*/

//Запустить блокнот
/*Console.WriteLine("Запустить блокнот");
Console.ReadLine();
Process.Start("notepad.exe");

//
Console.WriteLine("Press enter to start notepad ...");
Console.ReadLine();
Process notepad = Process.Start("notepad.exe");

Console.WriteLine("Press enter to stop notepad ...");
Console.ReadLine();

if (notepad.HasExited)
{
    Console.WriteLine($"Process has already finished, exit time: " +
        $"{notepad.ExitTime}, exit code: {notepad.ExitCode}");
}
else
{
    notepad.Kill();
    Console.WriteLine("Killed");
}*/

// ЗАДАНИЕ: написать программу которая позволяет запустить процесс по названию
// и затем остановить его по таймеру (вводится название процесса и время жизни (ttl) в секундах)
// по истечению ttl если процесс завершился - вывести информацию о завершении
// иначе вызвать Kill

Console.Write("Введите имя процесса: ");
string processName = Console.ReadLine();

Console.Write("Введите время в секундах: ");
int time = int.Parse(Console.ReadLine());

try
{
    Process process = Process.Start(processName);
    Console.WriteLine($"Процесс запущен. PID: {process.Id}");

    Thread.Sleep(time * 1000);

    if (process.HasExited)
    {
        Console.WriteLine($"Процесс завершился сам. Код выхода: {process.ExitCode}");
    }
    else
    {
        Console.WriteLine("TTL истёк. Останавливаем процесс...");
        process.Kill();
        process.WaitForExit();//Остановить текущую программу и подождать пока завершится
        Console.WriteLine("Процесс остановлен.");
    }
}
catch
{
    Console.WriteLine("Процесса не существует");
}

