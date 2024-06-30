using Newtonsoft.Json;
using StockSymbolChecker.Models;
using System.Collections.Generic;
using System;
using System.Web.Mvc;
using Newtonsoft.Json.Serialization;
using StockSymbolChecker.Services;

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

            DateTime? dateFrom = null;
            DateTime? dateTo = null;

            switch (request.StockDate)
            {
                case "Today":
                    dateFrom = DateTime.Now.AddDays(-1);
                    dateTo = DateTime.Now;
                    break;

                case "Weekly":
                    dateTo = DateTime.Now;
                    dateFrom = DateTime.Now.AddDays(-7);
                    break;

                case "Monthly":
                    dateTo = DateTime.Now;
                    dateFrom = DateTime.Now.AddDays(-30);
                    break;

                case "Custom":
                    dateTo = request.DateTo;
                    dateFrom = request.DateFrom;
                    break;

                default:
                    break;
            }

            //Mock Data
            //var data = new MockStockService(request.StockSymbol, dateFrom, dateTo).GetData();

            ////Real Data
            var data = new MarketstackService(request.StockSymbol, dateFrom, dateTo).GetData();

            // TODO : make settings global

            var homeVm = new HomeVm
            {
                StockApiRoot = data
            };

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };

            var homeVmJson = JsonConvert.SerializeObject(homeVm, settings);

            return Content(homeVmJson, "application/json");
        }
    }
}