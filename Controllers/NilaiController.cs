using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http;
using LaC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaC.Controllers
{
    public class NilaiController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.data = GetNilaiAsync().Result;
            return View();
        }

        public IActionResult InstansiLuar()
        {
            ViewBag.data = GetNilaiAsync().Result;
            return View();
        }

        public async Task<Boolean> PostInputMhs(string nim, string nama)
        {
            return await $"http://localhost:8000/api/v1/lac/mahasiswa/add/{nim}/{nama}".GetJsonAsync<bool>();
        }

        public async Task<List<NilaiModel>> GetNilaiAsync()
        {
            return await "http://localhost:8000/api/v1/lac/nilai/getAll".GetJsonAsync<List<NilaiModel>>();
        }
    }
}