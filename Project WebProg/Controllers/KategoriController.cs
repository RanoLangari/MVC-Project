using Project_WebProg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_WebProg.Controllers
{
    public class KategoriController : Controller
    {
        public static List<KategoriModel> ListKategori = new List<KategoriModel>();
        // GET: Kategori
        public ActionResult Index()
        {
            if (ListKategori != null && ListKategori.Count > 0)
            {
                return View(ListKategori);
            }

            for (int i = 0; i < 10; i++)
            {
                KategoriModel NewKategori = new KategoriModel();
                NewKategori.IdKategori = "Kategori " + i;
                NewKategori.NamaKategori = "Nama Kategori " + i;
                NewKategori.Keterangan = "Keterangan " + i;
                ListKategori.Add(NewKategori);
            }


            return View(ListKategori);
        }
        public ActionResult Tambah()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Tambah(KategoriModel Kategori)
        {
            if (Kategori.IdKategori == null)
            {
                ViewBag.Message = string.Format("ID kategori tidak boleh kosong");
                return View();
            }
            else if (Kategori.IdKategori.Length != 5)
            {
                ViewBag.Message = string.Format("Panjang ID Kategori Harus 5 Karakter");
                return View();

            }
            else if (ListKategori.Any(x => x.IdKategori == Kategori.IdKategori))
            {
                ViewBag.Message = string.Format("Id Kategori Sudah Ada");
                return View();
            }
            else if (Kategori.IdKategori.Any(char.IsLetter))
            {
                ViewBag.Message = String.Format("ID Kategori Harus Angka");
                return View();
            }
            else
            {
                ListKategori.Add(Kategori);
                return RedirectToAction("Index");
            }

        }
        public ActionResult Edit(string IdKategori)
        {
            KategoriModel Kategori = ListKategori.Where(x => x.IdKategori == IdKategori).FirstOrDefault();
            return View(Kategori);
        }
        [HttpPost]
        public ActionResult Edit(KategoriModel Kategori)
        {
            if (ListKategori.Any(x => x.IdKategori == Kategori.IdKategori))
            {

                KategoriModel KategoriEdit = ListKategori.Where(x => x.IdKategori == Kategori.IdKategori).FirstOrDefault();
                KategoriEdit.NamaKategori = Kategori.NamaKategori;
                KategoriEdit.Keterangan = Kategori.Keterangan;
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = string.Format("IdKategori tidak ditemukan di dalam list");
                return View();
            }
            
        }
        public ActionResult Delete(string idkat)
        {
            KategoriModel HapusKategori = ListKategori.Where(x => x.IdKategori == idkat).FirstOrDefault();
            return View(HapusKategori);
        }
        [HttpPost]
        public ActionResult Delete(KategoriModel Kategori)
        {
            if (ListKategori.Any(x => x.IdKategori == Kategori.IdKategori))
            {
                KategoriModel HapusKategori = ListKategori.Where(x => x.IdKategori == Kategori.IdKategori).FirstOrDefault();
                ListKategori.Remove(HapusKategori);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = string.Format("Tidak bisa menghapus, IdKategori {0} tidak ditemukan", Kategori.IdKategori);
                return View();
            }
        }
    }
}