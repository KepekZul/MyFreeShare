using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyFreeShare.Models;

namespace MyFreeShare.Controllers
{
    public class HashString
    {
        public static string hash(string input)
        {
            UInt64 hashVal = 12345678912345678;
            for(int i=0; i<input.Length; i++)
            {
                hashVal += input[i];
                hashVal*= 12345678912345678;
            }
            return hashVal.ToString();
        }
    }
    public class AuthenticationController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(pengguna data)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MainDBContext())
                {
                    if (db.dataPengguna.Where(x => x.username == data.username).Any())
                    {
                        var dataUser = db.dataPengguna.First(x => x.username == data.username);
                        string passwordDB = dataUser.password;
                        if (HashString.hash(data.password) == passwordDB)
                        {
                            var identity = new ClaimsIdentity(new[]
                            {
                            new Claim("username", dataUser.username),
                            new Claim("email", dataUser.email)
                        },
                            "ApplicationCookie");
                            var ctx = Request.GetOwinContext();
                            var authmanager = ctx.Authentication;
                            authmanager.SignIn(identity);
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
            }
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult SignUp(pengguna data)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            using(var db= new MainDBContext())
            {
                if(!db.dataPengguna.Where(x=>x.username==data.username || x.email == data.email).Any())
                {
                    data.password = HashString.hash(data.password);
                    db.dataPengguna.Add(data);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Authentication");
                }
                else
                {
                    TempData["pesan"] = "username atau email telah terdaftar";
                    return View();
                }
            }
        }
        [AllowAnonymous]
        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authmanager = ctx.Authentication;
            authmanager.SignOut("ApplicationCookie");
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
                    TempData["pesan"] = "password lama salah";
                    return RedirectToAction("UpdatePassword", "Authentication");
                }
            }
        }
    }
}