using CryptocurrencyBank.Desktop.CryptocurrencyBankApi;
using CryptocurrencyBank.Desktop.MVVM.Core;
using CryptocurrencyBank.Desktop.MVVM.Models;
using CryptocurrencyBank.Desktop.MVVM.Models.ApiCommands.MoneyTransfer;
using CryptocurrencyBank.Desktop.MVVM.Views.MoneyTransferViews;
using CryptocurrencyBank.Desktop.WindowServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptocurrencyBank.Desktop.MVVM.ViewModels.MoneyTransfers
{
    class MoneyTransfersViewModel : ObserverableObject
    {
        private MoneyTransferHttpClient _client;
        private BalanceHttpClient _balanceClient;

        private MoneyTransfer _selected;
        private List<MoneyTransfer> _moneyTransfers;

        public MoneyTransfer Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged();
            }
        }

        public List<MoneyTransfer> Transfers 
        {
            get => _moneyTransfers; 
            set
            {
                _moneyTransfers = value;
                OnPropertyChanged();
            }
        }

        public MoneyTransfersViewModel(MoneyTransferHttpClient client, BalanceHttpClient balanceClient) 
        {
            _client = client;
            _balanceClient = balanceClient;

            Delete = new DelegateCommand(async x => await OnDeleteClick(), x => Selected is not null);
            Update = new DelegateCommand(async x => await OnUpdateClick(), x => Selected is not null);
            Create = new DelegateCommand(async x => await OnCreateClick(), x => true);

            RefreshTransfers();
        }

        public ICommand Create { get; set; }
        public ICommand Update { get; set; }
        public ICommand Delete { get; set; }

        private async Task OnDeleteClick()
        {
            var response = await _client.Delete(Selected.Id);

            if(response.IsSuccessStatusCode)
                RefreshTransfers();
        }

        private async Task OnCreateClick()
        {
            var guids = (await _balanceClient.GetAll()).Select(x => x.Id).ToList();

            var dataContext = new MoneyTransferCreatingViewModel(_client, guids);
            dataContext.Created += () => RefreshTransfers();

            WindowService.ShowAsDialog<MoneyTransferCreatingView>(dataContext);
        }

        private async Task OnUpdateClick()
        {
            var guids = (await _balanceClient.GetAll()).Select(x => x.Id).ToList();

            var dataContext = new MoneyTransferUpdatingViewModel(_client, guids, Selected);
            dataContext.Updated += () => RefreshTransfers();

            WindowService.ShowAsDialog<MoneyTransferUpdatingView>(dataContext);
        }

        private async void RefreshTransfers()
        {
            Transfers = (await _client.GetAll())?.ToList() ?? new();
        }
    }
}
