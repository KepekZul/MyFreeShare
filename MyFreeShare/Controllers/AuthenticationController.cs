using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFreeShare.Models;

namespace MyFreeShare.Controllers
{
    public class HashString
    {
        public static UInt64 hash(string input)
        {
            UInt64 hashVal = 12345678912345678;
            for(int i=0; i<input.Length; i++)
            {
                hashVal += input[i];
                hashVal*= 12345678912345678;
            }
            return hashVal;
        }
    }
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
        [HttpGet]
        public ActionResult UpdatePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdatePassword(FormCollection param)
        {
            using(var db = new MainDBContext())
            {
                var username = Session["username"].ToString();
                var user = db.dataPengguna.First(x => x.username == username );
                if(user.password== param["old_password"].ToString())
                {
                    user.password = param["new_password"].ToString();
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["pesan"] = "password ama salah";
                    return RedirectToAction("UpdatePassword", "Authentication");
                }
            }
        }
    }
}