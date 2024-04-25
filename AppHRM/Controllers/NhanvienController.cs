using AppHRM.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic; 

namespace AppHRM.Controllers
{
    public class NhanvienController : Controller
    {
        private MongoClient client = new MongoClient("mongodb://localhost:27017");

        public IActionResult Index()
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Nhanvien>("nhanvien");

            var nhanvien = table.Find(FilterDefinition<Nhanvien>.Empty).ToList(); 

            return View(nhanvien); 
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Nhanvien nhanVien)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Nhanvien>("nhanvien");
            nhanVien.Id = Guid.NewGuid().ToString();
            table.InsertOne(nhanVien);

            ViewBag.Mgs = "Nhân viên đã được thêm.";

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Nhanvien>("nhanvien");
            var nhanVien = table.Find(c => c.Id == id).FirstOrDefault();

            if(nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        [HttpPost]
        public ActionResult Edit(Nhanvien nhanVien)
        {

            if(string.IsNullOrEmpty(nhanVien.Id))
            {
                ViewBag.Mgs = "Hãy cung cấp ID";
                return View(nhanVien);
            }
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Nhanvien>("nhanvien");
            table.ReplaceOne(c => c.Id == nhanVien.Id, nhanVien);

            return RedirectToAction("Index");
        }

        public ActionResult Details(string id)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Nhanvien>("nhanvien");
            var nhanVien = table.Find(c => c.Id ==id ).FirstOrDefault();

            if(nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        public ActionResult Delete(string id)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Nhanvien>("nhanvien");
            var nhanVien = table.Find(c => c.Id == id).FirstOrDefault();

            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        [HttpPost]
        public ActionResult Delete(Nhanvien nhanVien)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Nhanvien>("nhanvien");
            table.DeleteOne(c => c.Id == nhanVien.Id);

            return RedirectToAction("Index");
        }
    }
}
