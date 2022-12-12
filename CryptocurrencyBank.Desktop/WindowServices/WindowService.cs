using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CryptocurrencyBank.Desktop.WindowServices
{
    class WindowService
    {
        public static TWindow ShowWindow<TWindow>(object dataContext) 
            where TWindow : Window, new()
        {
            var window = new TWindow();
            window.DataContext = dataContext;
            window.Show();

            return window;
        }

        public static TWindow ShowAsDialog<TWindow>(object dataContext)
            where TWindow : Window, new()
        {
            var window = new TWindow();
            window.DataContext = dataContext;
            window.ShowDialog();

            return window;
        }
    }
}
