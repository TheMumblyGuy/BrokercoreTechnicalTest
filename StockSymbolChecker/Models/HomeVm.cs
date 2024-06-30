﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockSymbolChecker.Models
{
    public class StockSearchRequest
    {
        public string StockSymbol { get; set; }
        public string StockDate { get; set; }

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }

    public class HomeVm
    {
        public string Symbol { get; set; }
        public StockApiRoot StockApiRoot { get; set; }
    }

    public class StockApiRoot
    {
        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }

        [JsonProperty("data")]
        public List<Datum> Data { get; set; }
    }

    public class Datum
    {
        [JsonProperty("open")]
        public double Open { get; set; }

        [JsonProperty("high")]
        public double High { get; set; }

        [JsonProperty("low")]
        public double Low { get; set; }

        [JsonProperty("close")]
        public double Close { get; set; }

        [JsonProperty("volume")]
        public double Volume { get; set; }

        [JsonProperty("adj_high")]
        public double AdjHigh { get; set; }

        [JsonProperty("adj_low")]
        public double AdjLow { get; set; }

        [JsonProperty("adj_close")]
        public double AdjClose { get; set; }

        [JsonProperty("adj_open")]
        public double AdjOpen { get; set; }

        [JsonProperty("adj_volume")]
        public double AdjVolume { get; set; }

        [JsonProperty("split_factor")]
        public double SplitFactor { get; set; }

        [JsonProperty("dividend")]
        public double Dividend { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("exchange")]
        public string Exchange { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }

    public class Pagination
    {
        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}