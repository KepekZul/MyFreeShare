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
            db.dataFile.Add(new Models.File { nama_file = filename, file_path = path, pengguna = Session["username"].ToString() });
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Cari(FormCollection param)
        {
            string[] query = param["query"].Split(' ');
            List<string> resultFile = new List<string>();
            using(var db =new MainDBContext())
            {
                foreach(var q in query)
                {
                    var resultQuery = db.dataFile.Where(x => x.nama_file.Contains(q)).ToList();
                    foreach(var r in resultQuery)
                    {
                        resultFile.AddRange(r.)
                    }
                }
            }
        }
    }
}