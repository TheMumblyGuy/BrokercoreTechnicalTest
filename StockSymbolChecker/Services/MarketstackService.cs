using Newtonsoft.Json;
using StockSymbolChecker.Exceptions;
using StockSymbolChecker.Models;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;

namespace StockSymbolChecker.Services
{
    public class MarketstackService
    {
        private static readonly string BaseUrl = "http://api.marketstack.com/v1/tickers";
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
                    HandleErrorResponse(response);
                    return null; // This will never be hit as HandleErrorResponse will throw an exception
                }
            }
        }

        private void HandleErrorResponse(HttpResponseMessage response)
        {
            string errorMessage = response.ReasonPhrase;
            var content = response.Content.ReadAsStringAsync().Result;
            var errorCode = JsonConvert.DeserializeObject<ApiErrorResponse>(content)?.Error?.Code;

            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedException("Unauthorized: Check your access key or activity of the account.");
                case HttpStatusCode.Forbidden:
                    if (errorCode == "https_access_restricted")
                    {
                        throw new HttpsAccessRestrictedException("HTTPS access is not supported on the current subscription plan.");
                    }
                    else if (errorCode == "function_access_restricted")
                    {
                        throw new FunctionAccessRestrictedException("The given API endpoint is not supported on the current subscription plan.");
                    }
                    break;

                case HttpStatusCode.NotFound:
                    if (errorCode == "invalid_api_function")
                    {
                        throw new InvalidApiFunctionException("The given API endpoint does not exist.");
                    }
                    else if (errorCode == "not_found_error")
                    {
                        throw new ResourceNotFoundException("Resource not found.");
                    }
                    break;

                case (HttpStatusCode)429:
                    if (errorCode == "too_many_requests")
                    {
                        throw new TooManyRequestsException("The given user account has reached its monthly allowed request volume.");
                    }
                    else if (errorCode == "rate_limit_reached")
                    {
                        throw new RateLimitReachedException("The given user account has reached the rate limit.");
                    }
                    break;

                case HttpStatusCode.InternalServerError:
                    throw new InternalErrorException("An internal error occurred.");
                default:
                    throw new MarketstackException($"Unexpected error: {response.ReasonPhrase}");
            }
        }

        private string BuildUrl(string symbol, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            var url = $"{BaseUrl}/{symbol}/eod?access_key={ApiKey}";

            if (dateFrom != null && dateTo != null)
            {
                var dateFromString = dateFrom.Value.ToString("yyyy-MM-dd");
                var dateToString = dateTo.Value.ToString("yyyy-MM-dd");
                url = url + $"&date_from={dateFromString}&date_to={dateToString}";
            }

            return url;
        }

        private class ApiErrorResponse
        {
            [JsonProperty("error")]
            public ApiError Error { get; set; }
        }

        private class ApiError
        {
            [JsonProperty("code")]
            public string Code { get; set; }
        }
    }
}