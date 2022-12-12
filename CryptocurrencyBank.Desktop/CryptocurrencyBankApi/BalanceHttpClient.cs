using CryptocurrencyBank.Desktop.MVVM.Models;
using CryptocurrencyBank.Desktop.MVVM.Models.ApiCommands.Balance;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CryptocurrencyBank.Desktop.CryptocurrencyBankApi
{
    public class BalanceHttpClient
    {
        private const string controller = "Balance";
        public readonly HttpClient _client = new();
        
        public BalanceHttpClient(string baseUrl) 
        {
            _client.BaseAddress = new Uri($"{baseUrl}/{controller}/");
        }

        public async Task<IEnumerable<Balance>> GetAll()
        {
            var response = await _client.GetFromJsonAsync<IEnumerable<Balance>>("GetAll");
            return response;
        }

        public async Task<Balance> GetById(Guid id)
        {
            var response = await _client.GetFromJsonAsync<Balance>($"Get/{id}");
            return response;
        }

        public async Task<HttpResponseMessage> Update(BalanceUpdateCommand updateCommand)
        {
            return await _client.PutAsync($"Update", updateCommand, new JsonMediaTypeFormatter());
        }

        public async Task<HttpResponseMessage> Create(BalanceCreateCommand createCommand)
        {
            return await _client.PostAsync($"Create", createCommand, new JsonMediaTypeFormatter());
        }

        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            return await _client.DeleteAsync($"Delete/{id}");
        }
    }
}
