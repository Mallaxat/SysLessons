using DZ_dispatcher.Windows;
using Lesson_2DZ_dicpatcher.Model;
using Lesson_2DZ_dicpatcher.Servises;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;
using Application = System.Windows.Application;


namespace Lesson_2DZ_dicpatcher.VM
{
    public class VM_Main : PropertyChange
    {
        //Свойства
        private readonly DispatcherTimer timer=new DispatcherTimer();
       
        private Dictionary<int, ProcessInfo> _listProcessDictionary = new();
        public Dictionary<int, ProcessInfo> ListProcessDictionary
        {
            get => _listProcessDictionary;
            private set=>SetProperty(ref _listProcessDictionary, value);
        }
        
        private ObservableCollection< ProcessInfo> _listProcess = new();
        public ObservableCollection<ProcessInfo> ListProcess
        {
            get => _listProcess;
            private set => SetProperty(ref _listProcess, value);
        }

        private ProcessInfo _selectProcess=new();
        public ProcessInfo SelectProcess
        {
            get => _selectProcess;
            set=>SetProperty(ref _selectProcess, value);
        }

        private Thread ThreadUpdate;
        //Класс токенов,  которые посылают информацию о завершении задачи
        CancellationTokenSource token=new CancellationTokenSource();
        //Команды
        public ICommand cAddProcess{ get; }

        public ICommand cDelete { get; }
        

        //Конструктор
        public VM_Main()
        {
            cAddProcess = new RelayCommand(_ =>
                {
                    AddProcess();
                });
            cDelete = new RelayCommand(_ =>
            {
                DeleteProcess();
            });

            ThreadUpdate = new Thread(Update)
            {
                IsBackground = true
            };
            ThreadUpdate.Start();
        }

        //Методы
        private void Update()
        {
            while (true)
            {
                UpdateWork();
                Application.Current.Dispatcher.Invoke(() => UpdateCollection());
                Thread.Sleep(5000);

            }
        }
        private void UpdateWork()
        {
            ListProcessDictionary.Clear();

            List<Process> bufListProcess = Process.GetProcesses().ToList();
            //Время сейчас
            DateTime dateTimeStart = DateTime.Now;

            double useCPU, useTime = 0;
            //Делаем первый замер и заполняем данные
            foreach (Process p in bufListProcess)
            {
                try
                {

                    p.Refresh();
                    ListProcessDictionary[p.Id] = new ProcessInfo
                    {
                        Id = p.Id,
                        ProcessName = p.ProcessName,
                        StartProc = p.TotalProcessorTime,
                        OZY = p.WorkingSet64 / 1024d / 1024d
                    };
                }
                catch { }
            }
            //Поток ждет секунду
            Thread.Sleep(1000);
            //Время после замера
            DateTime dateTimeEnd = DateTime.Now;
            //Второй замер

            foreach (Process p in bufListProcess)
            {
                try
                {
                    p.Refresh();
                    TimeSpan StopProcTest, StartProcTest;

                    StopProcTest=ListProcessDictionary.FirstOrDefault(x => x.Key == p.Id).Value.StopProc = p.TotalProcessorTime;
                    StartProcTest = ListProcessDictionary.FirstOrDefault(x => x.Key == p.Id).Value.StartProc;
                    
                    useTime = (dateTimeEnd - dateTimeStart).TotalMilliseconds;
                    useCPU = (StopProcTest - StartProcTest).TotalMilliseconds;
                   
                    ListProcessDictionary.FirstOrDefault(x => x.Key == p.Id).Value.CPU = useCPU / (Environment.ProcessorCount * useTime);// * 100;
                    
                }
                catch { }
            }
        }
      
        private void UpdateCollection()
        {
            ListProcess.Clear();
            foreach (var item in ListProcessDictionary)
            {
                ListProcess.Add(item.Value);
            }
        }

        private void AddProcess()
        {
            try
            {
                string result = String.Empty;
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = "files(*.exe)|*.exe|All files(*.*)|*.*";
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    result = openFile.FileName;
                    Process.Start(result);
                }
            }
            catch { }

        }

        public void DeleteProcess()
        {
            if (SelectProcess == null) return;
    
                Process pr = Process.GetProcessById(SelectProcess.Id);
                pr.Kill(true);//завершить со всеми дочерними
                pr.WaitForExit();//дождаться завершения
        }

        public void OpenInfo()
        {
            VM_InfoW _vm = new VM_InfoW(SelectProcess.Id, Process.GetProcesses().ToList());
            Info inf = new Info(_vm);
            inf.ShowDialog();
        }
    }
    
}
