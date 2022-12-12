using CryptocurrencyBank.Desktop.MVVM.Models;
using CryptocurrencyBank.Desktop.MVVM.Models.ApiCommands.MoneyTransfer;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CryptocurrencyBank.Desktop.CryptocurrencyBankApi
{
    public class MoneyTransferHttpClient
    {
        private const string controller = "MoneyTransfer";
        private readonly HttpClient _client = new();

        public HttpClient Client => _client;

        public MoneyTransferHttpClient(string baseUrl) 
        { 
            _client.BaseAddress = new Uri($"{baseUrl}/{controller}/");
        }

        public async Task<HttpResponseMessage> Create(MoneyTransferCreateCommand createCommand)
        {
            return await _client.PostAsync($"Create", createCommand, new JsonMediaTypeFormatter());
        }

        public async Task<HttpResponseMessage> Update(MoneyTransferUpdateCommand updateCommand)
        {
            return await _client.PutAsync($"Update", updateCommand, new JsonMediaTypeFormatter());
        }

        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            return await _client.DeleteAsync($"Delete/{id}");
        }

        public async Task<IEnumerable<MoneyTransfer>> GetAll()
        {
            return await _client.GetFromJsonAsync<IEnumerable<MoneyTransfer>>($"GetAll");
        }

        public async Task<IEnumerable<MoneyTransfer>> GetByDate(DateTime date)
        {
            return await _client.GetFromJsonAsync<IEnumerable<MoneyTransfer>>($"GetByDate/{date}");
        }

        public async Task<MoneyTransfer> Get(Guid id)
        {
            return await _client.GetFromJsonAsync<MoneyTransfer>($"Get/{id}");
        }
    }
}
