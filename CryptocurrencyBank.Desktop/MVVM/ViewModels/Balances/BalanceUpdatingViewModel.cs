using CryptocurrencyBank.Desktop.CryptocurrencyBankApi;
using CryptocurrencyBank.Desktop.MVVM.Core;
using CryptocurrencyBank.Desktop.MVVM.Models;
using CryptocurrencyBank.Desktop.MVVM.Models.ApiCommands.Balance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptocurrencyBank.Desktop.MVVM.ViewModels.Balances
{
    public class BalanceUpdatingViewModel : ObserverableObject
    {
        private readonly BalanceHttpClient _balanceHttp;

        public event Action Updated;

        private BalanceUpdateCommand _balance = new();
        public BalanceUpdateCommand Balance
        {
            get => _balance;
            set
            {
                _balance = value;
                OnPropertyChanged();
            }
        }

        public BalanceUpdatingViewModel(BalanceHttpClient balanceHttp) 
        {
            _balanceHttp = balanceHttp;

            Update = new DelegateCommand(async x => await OnUpdateClick(), x => Balance.IsValid());
            Clear = new DelegateCommand(x => Balance = new() { Id = Balance.Id }, x => true);
        }

        public BalanceUpdatingViewModel(BalanceHttpClient balanceHttp, Balance balance)
            : this(balanceHttp)
        {
            (_balance.Id, _balance.Description, _) = balance;
        }

        public ICommand Update { get; set; }
        public ICommand Clear { get; set;}

        private async Task OnUpdateClick()
        {
            var response = await _balanceHttp.Update(Balance);

            if (response.IsSuccessStatusCode)
                Updated?.Invoke();
        }
    }
}
