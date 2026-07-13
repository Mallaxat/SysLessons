using System.IO.Pipes;

namespace DZ_3_ServerClient
{
    internal class Program
    {
        private const string PipeName = "TimePipe";

        static void Main(string[] args)
        {
            string mode;

            // Режим можно передать аргументом:
            // server или client.
            if (args.Length > 0)
            {
                mode = args[0].Trim().ToLower();
            }
            else
            {
                mode = SelectMode();
            }

            switch (mode)
            {
                case "1":
                case "server":
                    RunServer();
                    break;

                case "2":
                case "client":
                    RunClient();
                    break;

                default:
                    Console.WriteLine("Неизвестный режим работы.");
                    break;
            }
        }

        static string SelectMode()
        {
            while (true)
            {
                Console.WriteLine("Выберите режим запуска:");
                Console.WriteLine("1 — сервер");
                Console.WriteLine("2 — клиент");
                Console.Write("Ваш выбор: ");

                string? input = Console.ReadLine()?.Trim().ToLower();

                if (input == "1" || input == "server")
                {
                    return "server";
                }

                if (input == "2" || input == "client")
                {
                    return "client";
                }

                Console.WriteLine("Неверный выбор. Введите 1 или 2.");
                Console.WriteLine();
            }
        }

        static void RunServer()
        {
            Console.WriteLine();
            Console.WriteLine("Сервер запущен.");
            Console.WriteLine("Ожидание запросов от клиента...");

            bool isRunning = true;

            while (isRunning)
            {
                using NamedPipeServerStream pipeServer =
                    new NamedPipeServerStream(
                        PipeName,
                        PipeDirection.InOut,
                        1,
                        PipeTransmissionMode.Byte,
                        PipeOptions.None);

                try
                {
                    pipeServer.WaitForConnection();

                    using StreamReader reader = new StreamReader(pipeServer);
                    using StreamWriter writer = new StreamWriter(pipeServer)
                    {
                        AutoFlush = true
                    };

                    string? request = reader.ReadLine();

                    switch (request)
                    {
                        case "time":
                            string currentTime =
                                DateTime.Now.ToString("HH:mm:ss dd.MM.yyyy");

                            Console.WriteLine("Получен запрос времени.");
                            Console.WriteLine(
                                "Текущее системное время: " + currentTime);

                            writer.WriteLine(currentTime);
                            break;

                        case "exit":
                            Console.WriteLine(
                                "Получена команда завершения сервера.");

                            writer.WriteLine("Сервер завершает работу.");
                            isRunning = false;
                            break;

                        default:
                            Console.WriteLine("Получен неизвестный запрос.");
                            writer.WriteLine("Неизвестная команда.");
                            break;
                    }
                }
                catch (IOException exception)
                {
                    Console.WriteLine(
                        "Ошибка соединения с клиентом: " +
                        exception.Message);
                }

                if (isRunning)
                {
                    Console.WriteLine("Ожидание следующего запроса...");
                }
            }

            Console.WriteLine("Сервер остановлен.");
        }

        static void RunClient()
        {
            Console.WriteLine();
            Console.WriteLine("Клиент запущен.");
            Console.WriteLine("Нажмите Enter, чтобы запросить время.");
            Console.WriteLine("Введите exit, чтобы завершить сервер.");

            while (true)
            {
                Console.Write("> ");
                string? input = Console.ReadLine()?.Trim().ToLower();

                string request = input == "exit" ? "exit" : "time";

                try
                {
                    using NamedPipeClientStream pipeClient =
                        new NamedPipeClientStream(
                            ".",
                            PipeName,
                            PipeDirection.InOut);

                    Console.WriteLine("Подключение к серверу...");

                    // Ожидание подключения не более пяти секунд.
                    pipeClient.Connect(5000);

                    using StreamReader reader = new StreamReader(pipeClient);
                    using StreamWriter writer = new StreamWriter(pipeClient)
                    {
                        AutoFlush = true
                    };

                    writer.WriteLine(request);

                    string? response = reader.ReadLine();

                    Console.WriteLine(
                        "Ответ сервера: " +
                        (response ?? "Ответ не получен."));
                }
                catch (TimeoutException)
                {
                    Console.WriteLine(
                        "Не удалось подключиться к серверу. " +
                        "Убедитесь, что сервер запущен.");
                }
                catch (IOException exception)
                {
                    Console.WriteLine(
                        "Ошибка соединения с сервером: " +
                        exception.Message);
                }

                if (request == "exit")
                {
                    break;
                }
            }

            Console.WriteLine("Завершение работы.");
        }
    }
}
