using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl.Http;
using LaC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Pendaftaran()
        {
            ViewBag.data = GetHistoryAsync().Result;
            return View();
        }

        public IActionResult Nilai()
        {
            ViewBag.data = GetNilaiAsync().Result;
            return View();
        }

        public IActionResult Keanggotaan()
        {
            ViewBag.data = GetKeanggotaanAsync().Result;
            return View();
        }

        [HttpPost]
        public IActionResult addNilai(string nim, string nama, string tipetes, string tipepeserta, string tanggal, string jenis, string ruangan, string total)
        {
            var x = AddNilaiAsync(nim, nama, tipetes, tipepeserta, tanggal, jenis, ruangan, total).Result;
            ViewBag.data = GetNilaiAsync().Result;
            return View("Nilai");
        }
        
        [HttpPost]
        public IActionResult addKeanggotaan(string nim, string nama, string status, string tanggal, string pembayaran)
        {
            var x = AddKeanggotaanAsync(nim, nama, status, tanggal, pembayaran).Result;
            ViewBag.data = GetKeanggotaanAsync().Result;
            return View("Keanggotaan");
        }

        [HttpPost]
        public IActionResult editKeanggotaan(string dataid, string status, string tanggal, string pembayaran)
        {
            var x = EditKeanggotaanAsync(dataid, status, tanggal, pembayaran).Result;
            ViewBag.data = GetKeanggotaanAsync().Result;
            if (x)
            {
                return View("Keanggotaan");
            }
            return View("Keanggotaan");
        }

        [HttpPost]
        public IActionResult deleteKeanggotaan(string iduser)
        {
            var x = DeleteKeanggotaanAsync(iduser).Result;
            ViewBag.data = GetKeanggotaanAsync().Result;
            if (x)
            {
                return View("Keanggotaan");
            }
                return View("Keanggotaan");
        }

        [HttpPost]
        public IActionResult editNilai(string dataid, string total)
        {
            var x = EditNilaiAsync(dataid, total).Result;
            ViewBag.data = GetNilaiAsync().Result;
            if (x)
            {
                return View("Nilai");
            }
            return View("Nilai");
        }

        [HttpPost]
        public IActionResult deleteNilai(string iduser)
        {
            var x = DeleteNilaiAsync(iduser).Result;
            ViewBag.data = GetNilaiAsync().Result;
            if (x)
            {
                return View("Nilai");
            }
            return View("Nilai");
        }

        [HttpPost]
        public IActionResult editPendaftaran(string dataid, bool payment, bool agree)
        {
            var x = editDaftar(dataid, payment, agree).Result;
            ViewBag.data = GetHistoryAsync().Result;
            if (!x)
            {
                return View("Pendaftaran");
            }
            return View("Pendaftaran");
        }

        [HttpPost]
        public IActionResult deletePendaftaran(string iduser)
        {
            var x = deleteDaftar(iduser).Result;
            ViewBag.data = GetHistoryAsync().Result;
            if (x)
            {
                return View("Pendaftaran");
            }
            return View("Pendaftaran");
        }

        //DAFTAR RELATED

        public async Task<bool> deleteDaftar(string dataid)
        {
            return await $"http://localhost:8000/api/v1/lac/history/delete/{dataid}".GetJsonAsync<bool>();
        }

        public async Task<bool> editDaftar(string dataid, bool payment, bool agree)
        {
            int newPay = payment ? 1 : 0;
            int newAgree = agree ? 1 : 0;
            return await $"http://localhost:8000/api/v1/lac/history/edit/{dataid}/{newPay}/{newAgree}".GetJsonAsync<bool>();
        }

        public async Task<List<HistoryModel>> GetHistoryAsync()
        {
            return await "http://localhost:8000/api/v1/lac/history/getAll".GetJsonAsync<List<HistoryModel>>();
        }

        //NILAI RELATED

        public async Task<List<NilaiModel>> GetNilaiAsync()
        {
            return await "http://localhost:8000/api/v1/lac/nilai/getAll".GetJsonAsync<List<NilaiModel>>();
        }

        public async Task<Boolean> EditNilaiAsync(string Id, string nilaitotal)
        {
            return await $"http://localhost:8000/api/v1/lac/nilai/edit/{Id}/{nilaitotal}".GetJsonAsync<bool>();
        }

        public async Task<Boolean> DeleteNilaiAsync(string Id)
        {
            return await $"http://localhost:8000/api/v1/lac/nilai/delete/{Id}".GetJsonAsync<bool>();
        }

        public async Task<Boolean> AddNilaiAsync(string nim, string nama, string tipetes, string tipepeserta, string tanggal, string jenis, string ruangan, string total)
        {
            string newDate = Convert.ToDateTime(tanggal).ToString("yyyy-MM-dd");
            return await $"http://localhost:8000/api/v1/lac/nilai/add/{newDate}/{tipetes}/{nim}/{nama}/{tipepeserta}/{ruangan}/{total}/{jenis}".GetJsonAsync<bool>();
        }

        //KEANGGOTAAN RELATED
        public async Task<List<KeanggotaanModel>> GetKeanggotaanAsync()
        {
            return await $"http://localhost:8000/api/v1/lac/membership/getAll".GetJsonAsync<List<KeanggotaanModel>>();
        }

        public async Task<Boolean> EditKeanggotaanAsync(string nim, string status, string expire, string pembayaran)
        {
            return await $"http://localhost:8000/api/v1/lac/membership/edit/{nim}/{status}/{expire}/{pembayaran}".GetJsonAsync<bool>();
        }

        public async Task<Boolean> DeleteKeanggotaanAsync(string nim)
        {
            return await $"http://localhost:8000/api/v1/lac/membership/delete/{nim}".GetJsonAsync<bool>();
        }

        public async Task<Boolean> AddKeanggotaanAsync(string nim, string nama, string status, string tanggal, string pembayaran)
        {
            string newDate = Convert.ToDateTime(tanggal).Date.ToString("yyyy-MM-dd");
            return await $"http://localhost:8000/api/v1/lac/membership/add/{nim}/{nama}/{status}/{newDate}/{pembayaran}".GetJsonAsync<bool>();
        }
    }
}