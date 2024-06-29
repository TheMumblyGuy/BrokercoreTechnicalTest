using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StockSymbolChecker.Models;
using System;
using System.Configuration;
using System.Net.Http;

namespace StockSymbolChecker.Services
{
    // TODO : cache
    public class MarketstackService
    {
        private readonly StockSearchRequest request;

        public MarketstackService(StockSearchRequest request)
        {
            this.request = request;
        }

        private static readonly string BaseUrl = "http://api.marketstack.com/v1/eod";
        private static readonly string ApiKey = ConfigurationManager.AppSettings["MarketStackApiKey"];

        public StockApiRoot GetData()
        {
            string url = BuildUrl(request.StockSymbol);

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                   
                    var stockApiRootObj = JsonConvert.DeserializeObject<StockApiRoot>(responseBody);

                    return stockApiRootObj;
                }
                else
                {
                    throw new Exception($"Error fetching data from MarketStack API: {response.ReasonPhrase}");
                }
            }
        }

        private string BuildUrl(string symbol, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            var url = $"{BaseUrl}?access_key={ApiKey}&symbols={symbol}";

            if (dateFrom != null && dateTo != null)
            {
                url = url + $"&date_from={dateFrom}&date_to={dateTo}";
            }

            return url;
        }

        // TODO : be called from controller
        // TODO : create method to get weekly (figure out last 7 days)
        // TODO : create method to get monthly (figure out last 30 days)
    }
}