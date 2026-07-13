using DZ_dispatcher.Windows;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lesson_2DZ_dicpatcher
{
    /// <summary>
    /// Логика взаимодействия для Info.xaml
    /// </summary>
    public partial class Info : Window
    {
        VM_InfoW _vm;
        public Info(VM_InfoW inf)
        {
            InitializeComponent();
            this._vm = inf;
            DataContext = _vm;
        }
    }
}
