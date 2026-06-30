using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace DZ_dispatcher.Windows
{
    public class VM_InfoW:INotifyPropertyChanged
    {

        //Свойства
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }
        private string _processName = "";
        public string ProcessName
        {
            get => _processName;
            set
            {
                _processName = value;
                OnPropertyChanged();
            }
        }

        private double _memoryMb;
        public double MemoryMb
        {
            get => _memoryMb;
            set
            {
                _memoryMb = value;
                OnPropertyChanged();
            }
        }
        private int _threadsCount;
        public int ThreadsCount
        {
            get => _threadsCount;
            set
            {
                _threadsCount = value;
                OnPropertyChanged();
            }
        }

        private DateTime _startTime; 
        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                _startTime = value;
                OnPropertyChanged();
            }
        }

        public VM_InfoW(int id, List<Process> processes)
        {
            Process? process = null;

            try
            {
                process = processes.FirstOrDefault(x => x.Id == id);
            }
            catch
            {
                // Один из процессов мог завершиться
            }

            if (process == null)
                throw new InvalidOperationException($"Процесс с ID {id} не найден.");

            LoadProcessInfo(process);
        }

        //Методы
        private void LoadProcessInfo(Process process)
        {
            try
            {
                process.Refresh();

                Id = process.Id;
                ProcessName = process.ProcessName;
                MemoryMb = process.WorkingSet64 / 1024d / 1024d;
                ThreadsCount = process.Threads.Count;
                StartTime = process.StartTime;
            }
            catch {}
        }

        //Интерфейс
        void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
