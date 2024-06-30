using StockSymbolChecker.Exceptions;
using StockSymbolChecker.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace StockSymbolChecker.Services
{
    // TODO : make mock service better by generating less static data
    public class MockStockService
    {
        private readonly string symbol;
        private readonly DateTime? dateFrom;
        private readonly DateTime? dateTo;

        public MockStockService(string symbol, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            this.symbol = symbol;
            this.dateFrom = dateFrom;
            this.dateTo = dateTo;
        }

        public StockApiRoot GetData()
        {
            //throw new ResourceNotFoundException("Resource not found.");
            var mockData = new StockApiRoot
            {
                Pagination = new Pagination { Limit = 100, Offset = 0, Count = 100, Total = 9944 },
                Data = new Data
                {
                    Name = "Agilent Technologies Inc",
                    Symbol = "A",
                    Country = null,
                    HasIntraday = false,
                    HasEod = true,
                    //Eod = new List<Eod>()
                    Eod = GetDummyData()
                }
            };

            // For testing slow responses so I can test the loading icon
            Thread.Sleep(2000);

            return mockData;
        }

        // Dummy data
        public static List<Eod> GetDummyData() => new List<Eod>
                    {
                        new Eod
                        {
                            Open = 131.66,
                            High = 133.039,
                            Low = 128.39,
                            Close = 129.63,
                            Volume = 9817612,
                            AdjHigh = 133.039,
                            AdjLow = 128.39,
                            AdjClose = 129.63,
                            AdjOpen = 131.66,
                            AdjVolume = 9817612,
                            SplitFactor = 1,
                            Dividend = 0,
                            Symbol = "A",
                            Exchange = "XNYS",
                            Date = DateTime.Parse("2024-06-28T00:00:00+0000")
                        },
                        new Eod
                        {
                            Open = 132.2,
                            High = 132.99,
                            Low = 130.58,
                            Close = 130.8,
                            Volume = 1897700,
                            AdjHigh = 132.99,
                            AdjLow = 130.58,
                            AdjClose = 130.8,
                            AdjOpen = 132.2,
                            AdjVolume = 1897978,
                            SplitFactor = 1,
                            Dividend = 0,
                            Symbol = "A",
                            Exchange = "XNYS",
                            Date = DateTime.Parse("2024-06-27T00:00:00+0000")
                        },
                        new Eod
                        {
                            Open = 133.99,
                            High = 135.21,
                            Low = 132.76,
                            Close = 133.09,
                            Volume = 1812459,
                            AdjHigh = 135.21,
                            AdjLow = 132.76,
                            AdjClose = 133.09,
                            AdjOpen = 133.99,
                            AdjVolume = 1836777,
                            SplitFactor = 1,
                            Dividend = 0,
                            Symbol = "A",
                            Exchange = "XNYS",
                            Date = DateTime.Parse("2024-06-26T00:00:00+0000")
                        },
                        new Eod
                        {
                            Open = 135,
                            High = 136.01,
                            Low = 134.29,
                            Close = 134.69,
                            Volume = 2310800,
                            AdjHigh = 136.01,
                            AdjLow = 134.29,
                            AdjClose = 134.69,
                            AdjOpen = 135,
                            AdjVolume = 2310830,
                            SplitFactor = 1,
                            Dividend = 0,
                            Symbol = "A",
                            Exchange = "XNYS",
                            Date = DateTime.Parse("2024-06-25T00:00:00+0000")
                        },
                        new Eod
                        {
                            Open = 135.3,
                            High = 137.71,
                            Low = 134.34,
                            Close = 135.08,
                            Volume = 3339500,
                            AdjHigh = 137.71,
                            AdjLow = 134.34,
                            AdjClose = 135.08,
                            AdjOpen = 135.3,
                            AdjVolume = 3339551,
                            SplitFactor = 1,
                            Dividend = 0,
                            Symbol = "A",
                            Exchange = "XNYS",
                            Date = DateTime.Parse("2024-06-24T00:00:00+0000")
                        },
                        new Eod
                        {
                            Open = 132.85,
                            High = 134.5,
                            Low = 132.2,
                            Close = 133.25,
                            Volume = 4449700,
                            AdjHigh = 134.5,
                            AdjLow = 132.2,
                            AdjClose = 133.25,
                            AdjOpen = 132.85,
                            AdjVolume = 4461292,
                            SplitFactor = 1,
                            Dividend = 0,
                            Symbol = "A",
                            Exchange = "XNYS",
                            Date = DateTime.Parse("2024-06-21T00:00:00+0000")
                        },
                        new Eod
                        {
                            Open = 133.26,
                            High = 134.65,
                            Low = 132.53,
                            Close = 132.73,
                            Volume = 2886812,
                            AdjHigh = 134.65,
                            AdjLow = 132.53,
                            AdjClose = 132.73,
                            AdjOpen = 133.26,
                            AdjVolume = 2887363,
                            SplitFactor = 1,
                            Dividend = 0,
                            Symbol = "A",
                            Exchange = "XNYS",
                            Date = DateTime.Parse("2024-06-20T00:00:00+0000")
                        }
                    };

    }
}