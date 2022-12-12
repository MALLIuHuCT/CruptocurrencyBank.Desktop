using CryptocurrencyBank.Desktop.MVVM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyBank.Desktop.MVVM.Models.ApiCommands.MoneyTransfer
{
    public class MoneyTransferCreateCommand : ObserverableObject
    {
        private int _howMany;

        public MoneyTransferCreateCommand() { }

        public Guid From { get; set; }
        public Guid To { get; set; }
        public DateTime Date { get; init; } = DateTime.Now;
        public TransferType TransferType { get; set; }
        public ClientType Client { get; init; } = ClientType.Desktop;

        public int HowMany 
        {
            get => _howMany;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Value must be greater than zero", nameof(HowMany));

                _howMany = value;
            }
        }

    }
}
