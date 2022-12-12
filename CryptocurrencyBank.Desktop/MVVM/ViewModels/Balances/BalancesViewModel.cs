using CryptocurrencyBank.Desktop.CryptocurrencyBankApi;
using CryptocurrencyBank.Desktop.MVVM.Core;
using CryptocurrencyBank.Desktop.MVVM.Models;
using CryptocurrencyBank.Desktop.MVVM.Views.BalanceViews;
using CryptocurrencyBank.Desktop.WindowServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CryptocurrencyBank.Desktop.MVVM.ViewModels.Balances
{
    public class BalancesViewModel : ObserverableObject
    {
        private readonly BalanceHttpClient _balanceHttp;

        private List<Balance> _balances;
        private Balance _selected;

        public List<Balance> Balances 
        {
            get => _balances; 
            set
            {
                _balances = value;
                OnPropertyChanged();
            }
        }

        public Balance Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged();
            }
        }
        
        public BalancesViewModel() { }

        public BalancesViewModel(BalanceHttpClient balanceHttp)
        {
            _balanceHttp = balanceHttp;
            
            Delete = new DelegateCommand(async x => await OnDeleteClick(), x => _selected is not null);
            Update = new DelegateCommand(x => OnUpdateClick(), x => _selected is not null);
            Create = new DelegateCommand(x => OnCreateClick(), x => true);

            RefreshBalances();
        }

        public ICommand Delete { get; set; }
        public ICommand Create { get; set; }
        public ICommand Update { get; set; }

        private async void RefreshBalances()
            => Balances = (await _balanceHttp.GetAll())?.ToList() ?? new();

        private async Task OnDeleteClick()
        {
            var response = await _balanceHttp.Delete(Selected.Id);
            var message = response.IsSuccessStatusCode ? "Successfully deleted!" : "Request is not successfully.";
            
            MessageBox.Show(message);

            if(response.IsSuccessStatusCode)
                RefreshBalances();
        }

        public void OnCreateClick()
        {
            var creatingDataContext = new BalanceCreatingViewModel(_balanceHttp);
            creatingDataContext.Created += () => RefreshBalances();

            WindowService.ShowWindow<BalanceCreateView>(creatingDataContext);
        }

        public void OnUpdateClick()
        {
            var updatingDataContext = new BalanceUpdatingViewModel(_balanceHttp, Selected); 
            updatingDataContext.Updated+= () => RefreshBalances();

            WindowService.ShowWindow<BalanceUpdateView>(updatingDataContext);
        }
    }
}
