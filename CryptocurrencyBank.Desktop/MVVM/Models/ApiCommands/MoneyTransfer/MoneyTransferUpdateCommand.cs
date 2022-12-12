using CryptocurrencyBank.Desktop.MVVM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyBank.Desktop.MVVM.Models.ApiCommands.MoneyTransfer
{
    public class MoneyTransferUpdateCommand : ObserverableObject
    {
        public MoneyTransferUpdateCommand() { }

        private int _howMany;
        private bool _isValid;

        public Guid Id { get; set; }
        public Guid From { get; set; }
        public Guid To { get; set; }
        public DateTime Date { get; set; }
        public TransferType TransferType { get; set; }
        public ClientType Client { get; set; }

        public int HowMany 
        {
            get => _howMany;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Value of transfer must be greater than zero", nameof(HowMany));

                _howMany = value;
                OnPropertyChanged();
            }
        }

        public bool IsValid()
        {
            return HowMany > 0 &&
                To != Guid.Empty &&
                From != Guid.Empty &&
                To != From;
        }
    }
}
