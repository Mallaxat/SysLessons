using Lesson_2DZ_dicpatcher.Servises;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Lesson_2DZ_dicpatcher.Model
{
    public class ProcessInfo : PropertyChange
    {
        private int _id;
        public int Id
        {
            get => _id;
            set=>SetProperty(ref _id, value);
        }

        private string? _processName;
        public string? ProcessName
        {
            get => _processName;
            set=>SetProperty(ref _processName, value);
        }

        private double _ozy;
        public double OZY
        {
            get => _ozy;
            set => SetProperty(ref _ozy, value);
        }

        private TimeSpan _startProc;
        public TimeSpan StartProc
        {
            get => _startProc;
            set => SetProperty(ref _startProc, value);
        }

        private TimeSpan _stopProc;
        public TimeSpan StopProc
        {
            get => _stopProc;
            set => SetProperty(ref _stopProc, value);
        }


        private double _cpu;
        public double CPU
        {
            get => _cpu;
            set => SetProperty(ref _cpu, value);
        }


    }
}
