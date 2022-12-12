using CryptocurrencyBank.Desktop.CryptocurrencyBankApi;
using CryptocurrencyBank.Desktop.MVVM.Core;
using CryptocurrencyBank.Desktop.MVVM.Models;
using CryptocurrencyBank.Desktop.MVVM.Models.ApiCommands.MoneyTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CryptocurrencyBank.Desktop.MVVM.ViewModels.MoneyTransfers
{
    class MoneyTransferCreatingViewModel : ObserverableObject
    {
        private readonly MoneyTransferHttpClient _transferClient;

        private Guid _selectedFrom;
        private Guid _selectedTo;
        private List<Guid> _balanceGuids;

        private TransferType _selectedType;

        private bool _isValid;

        public event Action Created;

        public MoneyTransferCreateCommand MoneyTransfer { get; set; } = new();

        public List<TransferType> TransferTypes { get; } = new[] 
        {
            TransferType.transfer, 
            TransferType.repayment, 
            TransferType.payment 
        }.ToList();

        public TransferType SelectedType 
        {
            get => _selectedType; 
            set
            {
                _selectedType = value;
                OnPropertyChanged();
            }
        }

        public Guid SelectedFrom 
        {
            get => _selectedFrom; 
            set
            {
                if(value == SelectedTo)
                {
                    _isValid = false;
                    throw new ArgumentException("from balance must be not same with to balance", nameof(SelectedFrom));
                }

                _isValid = true;
                _selectedFrom = value;
                OnPropertyChanged();
            }
        }

        public Guid SelectedTo 
        {
            get => _selectedTo;
            set
            {
                if (value == SelectedFrom)
                {
                    _isValid = false;
                    throw new ArgumentException("To balance must be not same with from balance", nameof(SelectedTo));
                }

                _isValid = true;
                _selectedTo = value;
                OnPropertyChanged();
            }
        }

        public List<Guid> BalanceGuids 
        {
            get => _balanceGuids;
            set
            {
                _balanceGuids = value;
                OnPropertyChanged();
            }
        }
        
        public MoneyTransferCreatingViewModel(MoneyTransferHttpClient transferClient, IEnumerable<Guid> balanceGuids) 
        { 
            _transferClient = transferClient;
            BalanceGuids = balanceGuids.ToList();

            Create = new DelegateCommand(async x => await OnCreateClick(), x => _isValid);
        }

        public ICommand Create { get; set; }

        private async Task OnCreateClick()
        {
            MoneyTransfer.From = SelectedFrom; 
            MoneyTransfer.To = SelectedTo;
            MoneyTransfer.TransferType = SelectedType;

            var response = await _transferClient.Create(MoneyTransfer);
            var message = response.IsSuccessStatusCode ? "Transfer created!" : "Transfer not created.";

            if (response.IsSuccessStatusCode)
                Created?.Invoke();

            MessageBox.Show(message);
        }
    }
}
