using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using LaC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaC.Controllers
{
    public class PendaftaranController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DataPendaftaran()
        {
            ViewBag.data = testAsync().Result;
            return View();
        }

        public async Task<List<HistoryModel>> testAsync()
        {
            var getresp = await "http://localhost:8000/api/v1/lac/history/getAll".GetJsonAsync<List<HistoryModel>>();
            return getresp;
        }
    }
}