using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaC.Models
{
    public class HistoryModel
    {
        public int id { get; set; }
        public String NIM { get; set; }
        public String Nama { get; set; }
        public String Tgl_Test { get; set; }
        public String Tgl_Daftar { get; set; }
        public String Tipe_Test { get; set; }
        public String Tipe_Peserta { get; set; }
        public String Ruangan { get; set; }
        public Boolean Status_Pembayaran { get; set; }
        public Boolean Status_Persetujuan { get; set; }
        public string Jenis_History { get; set; }
    }
}
