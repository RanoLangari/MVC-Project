using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_WebProg.Models
{
    public class BarangModel
    {
        public string KodeBarang { get; set; } //key
        public string NamaBarang { get; set; }
        public string Keterangan { get; set; }
        public int Jumlah { get; set; }
        
    }
}