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
        [HttpPost]
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> files)
        {
            using (var db = new MainDBContext())
            {
                string publik = (Request.Form["publik"] == null) ? "0" : "1";
                foreach (var file in files)
                {
                    var filename = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/Uploads"),Guid.NewGuid().ToString()+Path.GetExtension(filename));
                    file.SaveAs(path);
                    db.dataFile.Add(new Models.File { nama_file = filename, file_path = path, pengguna = Session["username"].ToString(), waktu_terupload = DateTime.Now, terunduh = 0, public_file=publik });
                }
                db.SaveChanges();
            }
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
                    var resQ = db.dataFile.Where(x => x.nama_file.Contains(q) && x.public_file=="1").ToList();
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