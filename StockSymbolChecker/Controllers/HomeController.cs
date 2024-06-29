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
            //Mock Data
            var data = new MockStockService(request).GetData();

            //Real Data
            //var data = new MarketstackService(request).GetData();

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