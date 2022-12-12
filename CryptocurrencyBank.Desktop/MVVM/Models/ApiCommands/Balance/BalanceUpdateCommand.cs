using CryptocurrencyBank.Desktop.MVVM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyBank.Desktop.MVVM.Models.ApiCommands.Balance
{
    public class BalanceUpdateCommand : ObserverableObject
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }

        public bool IsValid() => true;
    }
}
