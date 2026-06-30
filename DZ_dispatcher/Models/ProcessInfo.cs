using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace DZ_dispatcher.Models
{
    public class ProcInform
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public double CPU { get; set; }
        public double OZY { get; set; }

        private static ProcessesInfo proc =new ProcessesInfo();
        private class ProcessesInfo
        {
            public List<Process> process { get; set; }
            public Dictionary<int, string> Name { get; private set; }
            public Dictionary<int, double> CPU { get; private set; }
            public Dictionary<int, double> OZY { get; private set; }


            //Конструктор
            public ProcessesInfo()
            {
                if (process == null) process = Process.GetProcesses().ToList();
            }

            public async Task CreateorUpdate()
            {
                CPU = await GetCPU(process);
                Name = GetNames(process);
                OZY = GetOZY(process);
            }

            //Методы
            //Метод, для определения процента занятости процессора
            public async Task<Dictionary<int, double>> GetCPU(List<Process> ListProcess)
            {
                List<TimeSpan> timeStart = new List<TimeSpan>();
                List<TimeSpan> timeEnd = new List<TimeSpan>();
                //Время сейчас
                DateTime dateTimeStart = DateTime.Now;

                foreach (var process in ListProcess)
                {
                    try
                    {
                        process.Refresh(); //обновить данные

                        //Сколько времени уже использовано
                        //TotalProcessorTime- фВозвращает общее время процессора для этого процесса
                        timeStart.Add(process.TotalProcessorTime);
                    } catch { }

                }
                await Task.Delay(1000);

                foreach (var process in ListProcess)
                {
                    try
                    {
                        process.Refresh();
                        timeEnd.Add(process.TotalProcessorTime);
                    } catch { }

                }

                DateTime dateTimeEnd = DateTime.Now;
                double useCPU, useTime = 0;
                useTime = (dateTimeEnd - dateTimeStart).TotalMilliseconds;

                Dictionary<int, double> result = new Dictionary<int, double>();

                for (int i = 0; i < ListProcess.Count; i++)
                {
                    try
                    {
                        useCPU = (timeEnd[i] - timeStart[i]).TotalMilliseconds;

                        double res = useCPU / (Environment.ProcessorCount * useTime) * 100;
                        // Environment.ProcessorCount — количество логических ядер.
                        result.Add(ListProcess[i].Id, res);

                    } catch { }


                }
                // 5 времени процессора/(2 ядра * на 6 секунд которые это ядра работали) * 100
                return result;
            }
            //Метод для получения имен процессов
            public Dictionary<int, string> GetNames(List<Process> ListProcess)
            {
                Dictionary<int, string> result = new Dictionary<int, string>();
                foreach (var item in ListProcess)
                {
                    try
                    {
                        result.Add(item.Id, item.ProcessName);
                    }
                    catch { }

                }
                return result;
            }

            public Dictionary<int, double> GetOZY(List<Process> ListProcess)
            {
                Dictionary<int, double> result = new Dictionary<int, double>();
                double startOZY = 0;

                foreach (var item in ListProcess)
                {
                    try
                    {
                        item.Refresh();
                        startOZY = item.WorkingSet64;
                        result.Add(item.Id, startOZY);
                    }
                    catch
                    {

                    }
                }
                return result;
            }

        }

        public static async Task<ObservableCollection<ProcInform>> LoadProc()
        {
            var result=new ObservableCollection<ProcInform>();
            ProcessesInfo inf=new ProcessesInfo();

            await inf.CreateorUpdate();

            var allKeys = inf.Name.Keys.Where(key => inf.CPU.ContainsKey(key) && inf.OZY.ContainsKey(key));

            foreach (var key in allKeys)
            {
                result.Add(new ProcInform
                {
                    Id = key,
                    Name = inf.Name.GetValueOrDefault(key)??string.Empty,
                    CPU=inf.CPU.GetValueOrDefault(key),
                    OZY=inf.OZY.GetValueOrDefault(key)
                });
            }
            return result;
        }

        public async static void Update()
        {
            await proc.CreateorUpdate();
        }


    }


    
}
