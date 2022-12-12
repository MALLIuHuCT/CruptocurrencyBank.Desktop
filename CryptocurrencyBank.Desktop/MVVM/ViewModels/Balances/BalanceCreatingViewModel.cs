using CryptocurrencyBank.Desktop.CryptocurrencyBankApi;
using CryptocurrencyBank.Desktop.MVVM.Core;
using CryptocurrencyBank.Desktop.MVVM.Models;
using CryptocurrencyBank.Desktop.MVVM.Models.ApiCommands.Balance;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CryptocurrencyBank.Desktop.MVVM.ViewModels.Balances
{
    public class BalanceCreatingViewModel : ObserverableObject
    {
        private readonly BalanceHttpClient _balanceHttp;

        private BalanceCreateCommand _balance = new();

        public event Action Created;

        public BalanceCreateCommand Balance 
        {
            get => _balance;
            set
            {
                _balance = value;
                OnPropertyChanged();
            }
        }

        public BalanceCreatingViewModel(BalanceHttpClient balanceHttp)
        {
            _balanceHttp = balanceHttp;

            Clear = new DelegateCommand(x => Balance = new(), x => true);
            Create = new DelegateCommand(async x => await OnCreateClick(), x => Balance.IsValid());
        }

        public ICommand Create { get; set; }
        public ICommand Clear { get; set; }

        private async Task OnCreateClick()
        {
            var response = await _balanceHttp.Create(Balance);

            if (response.IsSuccessStatusCode)
                Created?.Invoke();
        }
    }
}
