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
        private static readonly string BaseUrl = "http://api.marketstack.com/v1/eod";
        private static readonly string ApiKey = ConfigurationManager.AppSettings["MarketStackApiKey"];
        private readonly string symbol;
        private readonly DateTime? dateFrom;
        private readonly DateTime? dateTo;

        public MarketstackService(string symbol, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            this.symbol = symbol;
            this.dateFrom = dateFrom;
            this.dateTo = dateTo;
        }

        public StockApiRoot GetData()
        {
            string url = BuildUrl(this.symbol, this.dateFrom, this.dateTo);

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
                var dateFromString = dateFrom.Value.ToString("yyyy-MM-dd");
                var dateToString = dateTo.Value.ToString("yyyy-MM-dd");
                url = url + $"&date_from={dateFromString}&date_to={dateToString}";
            }

            return url;
        }
    }
}