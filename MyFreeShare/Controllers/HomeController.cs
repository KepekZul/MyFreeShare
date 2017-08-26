using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFreeShare.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            using (var db = new MainDBContext())
            {
                string username = Session["username"].ToString();
                var userItems = db.dataFile.Where(x => x.pengguna == username ).ToList();
                return View(userItems);
            }
        }
        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }
    }
}