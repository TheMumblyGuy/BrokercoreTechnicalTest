using Newtonsoft.Json;
using StockSymbolChecker.Models;
using System.Collections.Generic;
using System;
using System.Web.Mvc;

namespace StockSymbolChecker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchStock(StockSearchRequest request)
        {
            // Mock data - Replace this with actual logic to fetch data
            var mockData = new HomeVm
            {
                StockApiRoot = new StockApiRoot
                {
                    Pagination = new Pagination { Limit = 100, Offset = 0, Count = 100, Total = 9944 },
                    Data = new List<Datum>
                {
                    new Datum
                    {
                        Open = 129.8,
                        High = 133.04,
                        Low = 129.47,
                        Close = 132.995,
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
                        Date = DateTime.Now
                    }
                }
                }
            };

            return Json(mockData);
        }
    }
}