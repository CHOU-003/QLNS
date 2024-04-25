using AppHRM.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace AppHRM.Controllers
{
    public class PhongbanController : Controller
    {
        private MongoClient client = new MongoClient("mongodb://localhost:27017");

        public IActionResult Index()
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Phongban>("phongban");

            var phongbans = table.Find(FilterDefinition<Phongban>.Empty).ToList();

            return View(phongbans);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Phongban phongBan)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Phongban>("phongban");
            phongBan.Id = Guid.NewGuid().ToString();
            table.InsertOne(phongBan);

            ViewBag.Mgs = "Nhân viên đã được thêm.";

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Phongban>("phongban");
            var phongBan = table.Find(c => c.Id == id).FirstOrDefault();

            if (phongBan == null)
            {
                return NotFound();
            }

            return View(phongBan);
        }

        [HttpPost]
        public ActionResult Edit(Phongban phongBan)
        {

            if (string.IsNullOrEmpty(phongBan.Id))
            {
                ViewBag.Mgs = "Hãy cung cấp ID";
                return View(phongBan);
            }
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Phongban>("phongban");
            table.ReplaceOne(c => c.Id == phongBan.Id, phongBan);

            return RedirectToAction("Index");
        }

        public ActionResult Details(string id)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Phongban>("phongban");
            var phongBan = table.Find(c => c.Id == id).FirstOrDefault();

            if (phongBan == null)
            {
                return NotFound();
            }

            return View(phongBan);
        }

        public ActionResult Delete(string id)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Phongban>("phongban");
            var phongBan = table.Find(c => c.Id == id).FirstOrDefault();

            if (phongBan == null)
            {
                return NotFound();
            }

            return View(phongBan);
        }

        [HttpPost]
        public ActionResult Delete(Phongban phongBan)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Phongban>("phongban");
            table.DeleteOne(c => c.Id == phongBan.Id);

            return RedirectToAction("Index");
        }
    }
}
