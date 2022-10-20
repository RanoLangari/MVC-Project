using Project_WebProg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_WebProg.Controllers
{
    public class BarangController : Controller
    {
        public static List<BarangModel> listBarang = new List<BarangModel>();
        // GET: Barang
        public ActionResult Index()
        {
            if (listBarang != null && listBarang.Count > 0)
            {
                return View(listBarang);
            }

            for (int i = 0; i < 10; i++)
            {
                BarangModel newBarang = new BarangModel();
                newBarang.KodeBarang = "KodeBarang" + i;
                newBarang.NamaBarang = "NamaBarang" + i;
                newBarang.Keterangan = "Keterangan" + i;
                newBarang.Jumlah = i;
                listBarang.Add(newBarang);
            }


            return View(listBarang);
        }
        public ActionResult Tambah()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Tambah(BarangModel barang)
        {
            //jika panjang kode barang tidak sama dengan 5 karakter
            if (barang.KodeBarang == null)
            {

                ViewBag.Message = string.Format("Kode Barang Tidak Boleh Kosong");
                return View();
            }
            else if (barang.KodeBarang.Length != 5)
            {
                ViewBag.Message = string.Format("Kode Barang harus 5 karakter");
                return View();
            }
            //jika kode barang huruf
            else if (barang.KodeBarang.Any(char.IsLetter))
            {
                ViewBag.Message = string.Format("Kode Barang harus angka");
                return View();
            }
            //jika kode barang sudah ada
            else if (listBarang.Any(x => x.KodeBarang == barang.KodeBarang))
            {
                ViewBag.Message = string.Format("Kode Barang sudah ada");
                return View();
            }//jika kode barang ditambahakan tampilkan message
            
            else
            {                
                listBarang.Add(barang);
                ViewBag.Message = string.Format("Kode Barang telah Ditambahkan");
                return RedirectToAction("Index");

            }

        }
        public ActionResult Edit(string kodeBarang)
        {
            BarangModel barang = listBarang.Where(x => x.KodeBarang == kodeBarang).FirstOrDefault();
            return View(barang);
        }
        [HttpPost]
        public ActionResult Edit(BarangModel barang)

        {
            //jika kode barang ditemukan didalam list
            if (listBarang.Any(x => x.KodeBarang == barang.KodeBarang))
            {

                BarangModel barangEdit = listBarang.Where(x => x.KodeBarang == barang.KodeBarang).FirstOrDefault();
                barangEdit.NamaBarang = barang.NamaBarang;
                barangEdit.Keterangan = barang.Keterangan;
                barangEdit.Jumlah = barang.Jumlah;
                return RedirectToAction("Index");
            }
            //jika kode barang tidak ada dalam list

            else
            {
                ViewBag.Message = string.Format("Kode Barang Tidak ditemukan");
                return View();
            }
        }

        public ActionResult Delete(string kodeBarang)
        {
            BarangModel hapusbarang = listBarang.Where(x => x.KodeBarang == kodeBarang).FirstOrDefault();
            return View(hapusbarang);
        }
        [HttpPost]
        public ActionResult Delete(BarangModel Barang)
        {
            if (listBarang.Any(x => x.KodeBarang == Barang.KodeBarang))
            {
                BarangModel barangHapus = listBarang.Where(x => x.KodeBarang == Barang.KodeBarang).FirstOrDefault();
                listBarang.Remove(barangHapus);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = string.Format("Tidak Bisa Menghapus, Kode Barang {0} tidak ditemukan", Barang.KodeBarang);
                return View();
            }


        }
    }
}