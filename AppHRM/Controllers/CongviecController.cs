using AppHRM.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace AppHRM.Controllers
{
    public class CongviecController : Controller
    {
        private MongoClient client = new MongoClient("mongodb://localhost:27017");

        public IActionResult Index()
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Congviec>("congviec");

            var congviecs = table.Find(FilterDefinition<Congviec>.Empty).ToList();

            return View(congviecs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Congviec congViec)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Congviec>("congviec");
            congViec.Id = Guid.NewGuid().ToString();
            table.InsertOne(congViec);

            ViewBag.Mgs = "Nhân viên đã được thêm.";

            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Congviec>("congviec");
            var congViec = table.Find(c => c.Id == id).FirstOrDefault();

            if (congViec == null)
            {
                return NotFound();
            }

            return View(congViec);
        }

        [HttpPost]
        public ActionResult Edit(Congviec congViec)
        {

            if (string.IsNullOrEmpty(congViec.Id))
            {
                ViewBag.Mgs = "Hãy cung cấp ID";
                return View(congViec);
            }
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Congviec>("congviec");
            table.ReplaceOne(c => c.Id == congViec.Id, congViec);

            return RedirectToAction("Index");
        }

        public ActionResult Details(string id)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Congviec>("congviec");
            var congViec = table.Find(c => c.Id == id).FirstOrDefault();

            if (congViec == null)
            {
                return NotFound();
            }

            return View(congViec);
        }

        public ActionResult Delete(string id)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Congviec>("congviec");
            var congViec = table.Find(c => c.Id == id).FirstOrDefault();

            if (congViec == null)
            {
                return NotFound();
            }

            return View(congViec);
        }

        [HttpPost]
        public ActionResult Delete(Congviec congViec)
        {
            var database = client.GetDatabase("QuanlynhansuDB");
            var table = database.GetCollection<Congviec>("congviec");
            table.DeleteOne(c => c.Id == congViec.Id);

            return RedirectToAction("Index");
        }
    }
}
