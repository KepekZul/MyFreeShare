using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFreeShare.Models;

namespace MyFreeShare.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(pengguna data)
        {
            using(var db = new MainDBContext())
            {
                if (db.dataPengguna.Where(x => x.username == data.username).Any())
                {
                    var dataUser = db.dataPengguna.First(x => x.username == data.username);
                    string passwordDB = dataUser.password;
                    if (data.password == passwordDB)
                    {
                        Session["username"] = dataUser.nama;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["pesan"] = "password salah";
                    }
                }
                else
                {
                    TempData["pesan"] = "username tidak terdaftar";
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
    }
}