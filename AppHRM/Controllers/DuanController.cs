using AppHRM.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace AppHRM.Controllers
{
    public class DuanController : Controller
    {
        private MongoClient client = new MongoClient("mongodb://localhost:27017");

        public IActionResult Index()
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Duan>("duan");

            var duans = table.Find(FilterDefinition<Duan>.Empty).ToList();

            return View(duans);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Duan duAn)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Duan>("duan");
            duAn.Id = Guid.NewGuid().ToString();
            table.InsertOne(duAn);

            ViewBag.Mgs = "Nhân viên đã được thêm.";

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Duan>("duan");
            var duAn = table.Find(c => c.Id == id).FirstOrDefault();

            if (duAn == null)
            {
                return NotFound();
            }

            return View(duAn);
        }

        [HttpPost]
        public ActionResult Edit(Duan duAn)
        {

            if (string.IsNullOrEmpty(duAn.Id))
            {
                ViewBag.Mgs = "Hãy cung cấp ID";
                return View(duAn);
            }
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Duan>("duan");
            table.ReplaceOne(c => c.Id == duAn.Id, duAn);

            return RedirectToAction("Index");
        }

        public ActionResult Details(string id)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Duan>("duan");
            var duAn = table.Find(c => c.Id == id).FirstOrDefault();

            if (duAn == null)
            {
                return NotFound();
            }

            return View(duAn);
        }

        public ActionResult Delete(string id)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Duan>("duan");
            var duAn = table.Find(c => c.Id == id).FirstOrDefault();

            if (duAn == null)
            {
                return NotFound();
            }

            return View(duAn);
        }

        [HttpPost]
        public ActionResult Delete(Duan duAn)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Duan>("duan");
            table.DeleteOne(c => c.Id == duAn.Id);

            return RedirectToAction("Index");
        }
    }
}
