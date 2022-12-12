using CryptocurrencyBank.Desktop.CryptocurrencyBankApi;
using CryptocurrencyBank.Desktop.MVVM.ViewModels.Balances;
using CryptocurrencyBank.Desktop.MVVM.ViewModels.MoneyTransfers;
using CryptocurrencyBank.Desktop.MVVM.Views.BalanceViews;
using CryptocurrencyBank.Desktop.MVVM.Views.MoneyTransferViews;
using CryptocurrencyBank.Desktop.WindowServices;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CryptocurrencyBank.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(int.TryParse(PortUI.Text, out var port) is false)
            {
                MessageBox.Show("Invalid data!");
                return;
            }

            var connection = $"https://localhost:{port}/api";
            var balanceHttpClient = new BalanceHttpClient(connection);

            WindowService.ShowWindow<BalancesView>(new BalancesViewModel(balanceHttpClient));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(PortUI.Text, out var port) is false)
            {
                MessageBox.Show("Invalid data!");
                return;
            }

            var connection = $"https://localhost:{port}/api";
            var transferClient = new MoneyTransferHttpClient(connection);
            var balanceClient = new BalanceHttpClient(connection);

            WindowService.ShowWindow<MoneyTransfersView>(new MoneyTransfersViewModel(transferClient, balanceClient));
        }
    }
}
