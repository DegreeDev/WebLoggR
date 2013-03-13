using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLoggR.Models;

namespace WebLoggR.Controllers
{
    public class AppsController : Controller
    {
        private Entities _db;
        public AppsController()
        {
            _db = new Entities();
        }
        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
        public ActionResult Edit(Guid id)
        {
            Application app = _db.Applications.Find(id);

            return View(app);
        }

        [HttpPost]
        public ActionResult Edit(Application app)
        {
            Application app2 = _db.Applications.Find(app.ID);

            app2.Name = app.Name;



            int changes = _db.SaveChanges();
            return RedirectToAction("Account", "Home");
        }

    }
}
