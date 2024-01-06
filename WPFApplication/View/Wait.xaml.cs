using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFApplication.Services;

namespace WPFApplication.View
{
    /// <summary>
    /// Lógica interna para Wait.xaml
    /// </summary>
    public partial class Wait : Window
    {
        public Wait()
        {
            InitializeComponent();
            this.Topmost = true;
            this.ShowInTaskbar = false;
            this.WindowStyle = WindowStyle.None;
            
            btnCancel.IsEnabled = false;

            GlobalServices.Acc.CenterWindowOnScreen(this);
        }

        public void ActiveButton()
        {
            btnCancel.IsEnabled = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
