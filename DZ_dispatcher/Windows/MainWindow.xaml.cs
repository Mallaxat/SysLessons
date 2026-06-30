using DZ_dispatcher.Models;
using DZ_dispatcher.Windows;
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

namespace DZ_dispatcher.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VM_MainW _vm = new VM_MainW();
        public MainWindow()
        {
            InitializeComponent();

            DataContext = _vm;
        }

        private void tab_process_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _vm.SelectProcess = (ProcInform)tab_process.SelectedItem;
            _vm.OpenInfo();
        }
    }
}