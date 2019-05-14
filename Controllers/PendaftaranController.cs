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

        [HttpPost]
        public IActionResult Index(string tipePendaftar, string nim, string nama, string tipetes, string member, string date, string waktutes, string ruangan, string tujuan, string biaya, string total, string bukti)
        {
            var getresp = PostPendaftarTestAsync(tipePendaftar, nim, nama, tipetes, date, ruangan, "test").Result;
            if (getresp)
            {
                return View();
            }
            else
            {
                return Content("Wrong");
            }
        }

        [HttpPost]
        public IActionResult regisKursus(string tipePendaftar, string nim, string nama, string tipetes, string member, string date, string waktutes, string ruangan, string tujuan, string biaya, string total, string bukti)
        {
            var getresp = PostPendaftarTestAsync(tipePendaftar, nim, nama, tipetes, date, ruangan, "kursus").Result;
            if (getresp)
            {
                return View("Index");
            }
            else
            {
                return Content("Wrong");
            }
        }

        public async Task<Boolean> PostPendaftarTestAsync(string tipePendaftar, string nim, string nama, string tipetes, string date, string ruangan, string tipe)
        {
            var currDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
            var statusBayar = false;
            var statusagree = false;
            return await $"http://localhost:8000/api/v1/lac/history/add/{nim}/{nama}/{date}/{currDate}/{tipetes}/{tipePendaftar}/{ruangan}/{statusBayar}/{statusagree}/{tipe}".GetJsonAsync<bool>();
        }

        public async Task<List<HistoryModel>> testAsync()
        {
            var getresp = await "http://localhost:8000/api/v1/lac/history/getAll".GetJsonAsync<List<HistoryModel>>();
            return getresp;
        }
    }
}