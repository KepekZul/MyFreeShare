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
                        Session["username"] = dataUser.username;
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
        [HttpPost]
        public ActionResult SignUp(pengguna data)
        {
            using(var db= new MainDBContext())
            {
                if(!db.dataPengguna.Where(x=>x.username==data.username || x.email == data.email).Any())
                {
                    db.dataPengguna.Add(data);
                    db.SaveChanges();
                    Session["username"] = data.username;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["pesan"] = "username atau email telah terdaftar";
                    return View();
                }
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Authentication");
        }
    }
}