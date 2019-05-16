using Flurl.Http;
using LaC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaC.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("Nama") != null)
            {
                return View("~/Views/Home/Index.cshtml");
            }
            return View();
        }

        [HttpPost]
        public IActionResult GetLogin(string login)
        {
            var data = GetMahasiswa(login).Result;
            if (!data.Any())
            {
                return View("Index");
            }
            HttpContext.Session.SetString("Nama", data[0].Nama);
            return View("~/Views/Home/Index.cshtml");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }

        public async Task<List<MahasiswaModel>> GetMahasiswa(string nim)
        {
            return await $"http://localhost:8000/api/v1/lac/mahasiswa/get/{nim}".GetJsonAsync<List<MahasiswaModel>>();
        }
    }
}