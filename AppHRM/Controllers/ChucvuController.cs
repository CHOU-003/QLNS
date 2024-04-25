using AppHRM.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace AppHRM.Controllers
{
    public class ChucvuController : Controller
    {
        private MongoClient client = new MongoClient("mongodb://localhost:27017");

        public IActionResult Index()
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Chucvu>("chucvu");

            var chucvus = table.Find(FilterDefinition<Chucvu>.Empty).ToList();

            return View(chucvus);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Chucvu chucVu)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Chucvu>("chucvu");
            chucVu.Id = Guid.NewGuid().ToString();
            table.InsertOne(chucVu);

            ViewBag.Mgs = "Nhân viên đã được thêm.";

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Chucvu>("chucvu");
            var chucVu = table.Find(c => c.Id == id).FirstOrDefault();

            if (chucVu == null)
            {
                return NotFound();
            }

            return View(chucVu);
        }

        [HttpPost]
        public ActionResult Edit(Chucvu chucVu)
        {

            if (string.IsNullOrEmpty(chucVu.Id))
            {
                ViewBag.Mgs = "Hãy cung cấp ID";
                return View(chucVu);
            }
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Chucvu>("chucvu");
            table.ReplaceOne(c => c.Id == chucVu.Id, chucVu);

            return RedirectToAction("Index");
        }

        public ActionResult Details(string id)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Chucvu>("chucvu");
            var chucVu = table.Find(c => c.Id == id).FirstOrDefault();

            if (chucVu == null)
            {
                return NotFound();
            }

            return View(chucVu);
        }

        public ActionResult Delete(string id)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Chucvu>("chucvu");
            var chucVu = table.Find(c => c.Id == id).FirstOrDefault();

            if (chucVu == null)
            {
                return NotFound();
            }

            return View(chucVu);
        }

        [HttpPost]
        public ActionResult Delete(Chucvu chucVu)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Chucvu>("chucvu");
            table.DeleteOne(c => c.Id == chucVu.Id);

            return RedirectToAction("Index");
        }
    }
}
