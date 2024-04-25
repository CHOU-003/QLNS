using AppHRM.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace AppHRM.Controllers
{
    public class HopDongController : Controller
    {
        private MongoClient client = new MongoClient("mongodb://localhost:27017");

        public IActionResult Index()
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Hopdong>("hopdong");

            var hopdongs = table.Find(FilterDefinition<Hopdong>.Empty).ToList();

            return View(hopdongs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Hopdong hopDong)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Hopdong>("hopdong");
            hopDong.Id = Guid.NewGuid().ToString();
            table.InsertOne(hopDong);

            ViewBag.Mgs = "Nhân viên đã được thêm.";

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Hopdong>("hopdong");
            var hopDong = table.Find(c => c.Id == id).FirstOrDefault();

            if (hopDong == null)
            {
                return NotFound();
            }

            return View(hopDong);
        }

        [HttpPost]
        public ActionResult Edit(Hopdong hopDong)
        {

            if (string.IsNullOrEmpty(hopDong.Id))
            {
                ViewBag.Mgs = "Hãy cung cấp ID";
                return View(hopDong);
            }
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Hopdong>("hopdong");
            table.ReplaceOne(c => c.Id == hopDong.Id, hopDong);

            return RedirectToAction("Index");
        }

        public ActionResult Details(string id)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Hopdong>("hopdong");
            var hopDong = table.Find(c => c.Id == id).FirstOrDefault();

            if (hopDong == null)
            {
                return NotFound();
            }

            return View(hopDong);
        }

        public ActionResult Delete(string id)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Hopdong>("hopdong");
            var hopDong = table.Find(c => c.Id == id).FirstOrDefault();

            if (hopDong == null)
            {
                return NotFound();
            }

            return View(hopDong);
        }

        [HttpPost]
        public ActionResult Delete(Hopdong hopDong)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Hopdong>("hopdong");
            table.DeleteOne(c => c.Id == hopDong.Id);

            return RedirectToAction("Index");
        }
    }
}
