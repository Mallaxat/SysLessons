using Lesson_2DZ_dicpatcher.Model;
using Lesson_2DZ_dicpatcher.VM;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Lesson_2DZ_dicpatcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VM_Main _vm = new VM_Main();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _vm;
        }

        private void tab_process_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            _vm.SelectProcess = (ProcessInfo)tab_process.SelectedItem;
            _vm.OpenInfo();
        }
    }
}