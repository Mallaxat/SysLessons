using DZ_dispatcher.Models;
using DZ_dispatcher.Servises;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace DZ_dispatcher.Windows
{
    public class VM_MainW : INotifyPropertyChanged
    {
        //Свойства
        private ObservableCollection<ProcInform>? _listProcess;
        public ObservableCollection<ProcInform>? ListProcess
        {
            get => _listProcess;
            set
            {
                _listProcess = value;
                OnPropertyChanged();
            }
        }

        private ProcInform? _selectProcess;
        public ProcInform? SelectProcess
        {
            get => _selectProcess;
            set
            {
                _selectProcess = value;
                OnPropertyChanged();
            }
        }
        
        //команды
        public ICommand cUpdate { get; }

        //Конструктор
        public VM_MainW()
        {
            Create();
            cUpdate = new RelayCommand(_ =>
            {
                ProcInform.Update();
                MessageBox.Show("Успешно обнавлено");
            });
        }     
        
        private async void Create()
        {
            ListProcess = await ProcInform.LoadProc();
        }


        public void OpenInfo()
        {
            VM_InfoW _vm = new VM_InfoW(SelectProcess.Id,Process.GetProcesses().ToList());
            Info inf=new Info(_vm);
            inf.ShowDialog();
        }


        //Интерфейс
        void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
