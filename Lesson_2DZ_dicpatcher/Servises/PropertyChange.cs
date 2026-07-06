using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Lesson_2DZ_dicpatcher.Servises
{
    //Класс, который позволит устанавливать свойство
    //в любом другом классе без множественной реализации INotifyPropertyChanged
    public abstract class PropertyChange : INotifyPropertyChanged
    {
        public bool SetProperty<T>(ref T field,T value, [CallerMemberName] string? property=null )
        {
            //EqualityComparer - класс для сравнения разных объектов
            //Если значения одинаковые ничего не делаем
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(property));
            return true;
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
