using CryptocurrencyBank.Desktop.MVVM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyBank.Desktop.MVVM.Models.ApiCommands.Balance
{
    public class BalanceCreateCommand : ObserverableObject
    {
        private string _description;
        private int _value;

        private bool _isValid;

        public string? Description 
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public int Value 
        {
            get => _value;
            set
            {
                if (value < 0)
                {
                    _isValid = false;
                    throw new ArgumentException("value must be grater or equal to zero", nameof(Value));
                }

                _value = value;
                _isValid = true;

                OnPropertyChanged();
            }
        }

        public bool IsValid() => _isValid;
    }
}
