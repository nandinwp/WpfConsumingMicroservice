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
using WPFApplication.Services;
using WPFApplication.View;

namespace WPFApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FactoryAcc factory = new FactoryAcc();

        public MainWindow()
        {
            InitializeComponent();
            GlobalServices.Acc.CenterWindowOnScreen(this);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txbSeriais.Text != null && !txbSeriais.Text.Equals("Seriais"))
            {
                init.IsEnabled = false;
                Task minhaTarefa = GlobalServices.Acc.ShowWait(int.Parse(txbSeriais.Text));
                
                GlobalServices.faseRequest = 0;
                int mineTime = 0;

                while (!minhaTarefa.IsCompleted)
                {
                    mineTime++;
                    if(mineTime>=30 && GlobalServices.faseRequest != 2)
                    {
                        GlobalServices.waitWpp.ActiveButton();
                        break;
                    }
                    await Task.Delay(100); // Aguardar por um curto período antes de verificar novamente
                    
                }
                GlobalServices.faseRequest = 0;

                init.IsEnabled = true;
               // MessageBox.Show("Primeira aplicação", "Teste WPF", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
           

        }
    }
}