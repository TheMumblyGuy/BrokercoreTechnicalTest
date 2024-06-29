using StockSymbolChecker.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace StockSymbolChecker.Services
{
    public class MockStockService
    {
        private readonly StockSearchRequest request;

        public MockStockService(StockSearchRequest request)
        {
            this.request = request;
        }

        public StockApiRoot GetData()
        {
            var mockData = new StockApiRoot
            {
                Pagination = new Pagination { Limit = 100, Offset = 0, Count = 100, Total = 9944 },
                Data = new List<Datum>
                {
                    new Datum
                    {
                        Open = 215.805,
                        High = 216.07,
                        Low = 210.3,
                        Close = 210.62,
                        Volume = 106686703.0,
                        AdjHigh = 133.04,
                        AdjLow = 129.47,
                        AdjClose = 132.995,
                        AdjOpen = 129.8,
                        AdjVolume = 106686703.0,
                        SplitFactor = 1.0,
                        Dividend = 0.0,
                        Symbol = request.StockSymbol,
                        Exchange = "XNAS",
                        Date = new DateTime(2024, 6, 28)
                    },
                    new Datum
                    {
                        Open = 214.63,
                        High = 215.7395,
                        Low = 212.35,
                        Close = 214.1,
                        Volume = 106686703.0,
                        AdjHigh = 133.04,
                        AdjLow = 129.47,
                        AdjClose = 132.995,
                        AdjOpen = 129.8,
                        AdjVolume = 106686703.0,
                        SplitFactor = 1.0,
                        Dividend = 0.0,
                        Symbol = request.StockSymbol,
                        Exchange = "XNAS",
                        Date = new DateTime(2024, 6, 27)
                    },
                    new Datum
                    {
                        Open = 211.5,
                        High = 214.86,
                        Low = 210.64,
                        Close = 213.25,
                        Volume = 106686703.0,
                        AdjHigh = 133.04,
                        AdjLow = 129.47,
                        AdjClose = 132.995,
                        AdjOpen = 129.8,
                        AdjVolume = 106686703.0,
                        SplitFactor = 1.0,
                        Dividend = 0.0,
                        Symbol = request.StockSymbol,
                        Exchange = "XNAS",
                        Date = new DateTime(2024, 6, 26)
                    }
                }
            };

            Thread.Sleep(2000);

            return mockData;
        }
    }
}