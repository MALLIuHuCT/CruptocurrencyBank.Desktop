using CryptocurrencyBank.Desktop.CryptocurrencyBankApi;
using CryptocurrencyBank.Desktop.MVVM.Core;
using CryptocurrencyBank.Desktop.MVVM.Models;
using CryptocurrencyBank.Desktop.MVVM.Models.ApiCommands.MoneyTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptocurrencyBank.Desktop.MVVM.ViewModels.MoneyTransfers
{
    public class MoneyTransferUpdatingViewModel : ObserverableObject
    {
        private readonly MoneyTransferHttpClient _transferClient;

        private MoneyTransferUpdateCommand _transfer = new();
        private List<Guid> _balanceGuids;
        private Guid _from;
        private Guid _to;
        private TransferType _selectedType;

        public event Action Updated;
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

        public List<Guid> BalanceGuids
        {
            get => _balanceGuids;
            set => _balanceGuids = value;
        }

        public Guid From 
        {
            get => _from; 
            set
            {
                if (value == To)
                    throw new ArgumentException("Balances From and To must be differend", nameof(From));

                _from = value;
                OnPropertyChanged();
            }
        }

        public Guid To
        {
            get=> _to;
            set
            {
                if (value == From)
                    throw new ArgumentException("Balances From and To must be differend", nameof(To));

                _to = value;
                OnPropertyChanged();
            }
        }

        public MoneyTransferUpdateCommand MoneyTransfer 
        {
            get => _transfer; 
            set
            {
                _transfer = value;
                OnPropertyChanged();
            }
        }

        public MoneyTransferUpdatingViewModel(MoneyTransferHttpClient transferClient, 
            List<Guid> balanceGuids,
            MoneyTransfer transfer) 
        { 
            _transferClient = transferClient;

            _balanceGuids = balanceGuids;

            (_transfer.Id, _transfer.From, _transfer.To, _transfer.HowMany, _transfer.Date, _transfer.TransferType, _transfer.Client)
                = transfer;

            Update = new DelegateCommand(async x => await OnUpdateClick(), x => _transfer.IsValid());
        }

        public ICommand Update { get; set; }

        private async Task OnUpdateClick()
        {
            MoneyTransfer.From = From; 
            MoneyTransfer.To = To;

            var response = await _transferClient.Update(MoneyTransfer);

            if (response.IsSuccessStatusCode)
                Updated?.Invoke();
        }
    }
}
