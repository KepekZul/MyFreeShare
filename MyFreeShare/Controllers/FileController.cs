using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyFreeShare.Models;
using System.Web.Mvc;

namespace MyFreeShare.Controllers
{
    public class FileController : Controller
    {
        // GET: File
        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            var filename = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), filename);
            file.SaveAs(path);
            var db = new MainDBContext();
            db.dataFile.Add(new Models.File { nama_file = filename, file_path = path, pengguna = Session["username"].ToString(), waktu_terupload=DateTime.Now, terunduh=0 });
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Cari(FormCollection param)
        {
            using (var db = new MainDBContext())
            {
                List<Models.File> ResultFile = new List<Models.File>();
                string[] query = param["query"].Split(' ');
                foreach (var q in query)
                {
                    var resQ = db.dataFile.Where(x => x.nama_file.Contains(q)).ToList();
                    ResultFile.AddRange(resQ);
                }
                ResultFile = ResultFile.Distinct().ToList();
                return View(ResultFile);
            }
        }
        public FileResult Download(int id)
        {
            using(var db = new MainDBContext())
            {
                var berkas = db.dataFile.First(x => x.Id == id);
                berkas.terunduh += 1;
                db.SaveChanges();
                return File(berkas.file_path, System.Net.Mime.MediaTypeNames.Application.Octet, berkas.nama_file);
            }
        }
        public ActionResult Hapus(int id)
        {
            using(var db = new MainDBContext())
            {
                var file = db.dataFile.First(x => x.Id == id);
                try
                {
                    System.IO.File.Delete(file.file_path);
                    db.dataFile.Remove(file);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    TempData["pesan"] = "error terjadi, coba lagi";
                    return RedirectToAction("Index", "Home");
                }
            }
        }
    }
}