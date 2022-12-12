using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyBank.Desktop.MVVM.Models
{
    public class Balance
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public int Value { get; set; }

        public void Deconstruct(out Guid id, out string? description, out int value)
        {
            id = Id;
            description = Description;
            value = Value;
        }
    }
}
