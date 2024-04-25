using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using AppHRM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Thêm dòng này

namespace LANC.Controllers
{
    public class LoginController : Controller
    {
        private IMongoDatabase db;

        public LoginController()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            db = client.GetDatabase("QuanlynhansuDB");
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var collection = db.GetCollection<Quanly>("quanly");
            var quanLy = collection.Find(q => q.email == email).FirstOrDefault();


            if (quanLy != null)
            {
                if (password.Equals(quanLy.password))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Sai Mật Khẩu";
                    return View("Error");
                }
            }
            else
            {
                ViewBag.error = "Tài Khoản Không Tồn Tại !!!";
                return View("Index");
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("email"); // Và ở đây
            return RedirectToAction("Index");
        }
    }
}
