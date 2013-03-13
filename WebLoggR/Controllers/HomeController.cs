using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebLoggR.Filters;
using WebLoggR.Models;

namespace WebLoggR.Controllers
{
    [RequireSession]
    public class HomeController : Controller
    {
        private WebLoggR.Models.Entities _db;
        public HomeController()
        {
            _db = new Entities();
        }
        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Index()
        {
            ViewBag.AccountId = (Guid)Session["AccountId"];

            return View();
        }
        public ActionResult Tester()
        {
            ViewBag.AccountId = (Guid)Session["AccountId"];

            return View();
        }
        public ActionResult Account()
        {
            ViewBag.AccountId = (Guid)Session["AccountId"];

            return View();
        }
        [AllowAnonymous]
        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("LogIn");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogIn(string user, string password)
        {
            var tempUser = _db.Users.FirstOrDefault(x => x.Name == user && x.Password == password);

            if (tempUser != null)
            {
                FormsAuthentication.SetAuthCookie(user, true);
                Session.Add("AccountId", tempUser.Account);
                return RedirectToAction("Index");
            }

            return View("Login");
        }

        [AllowAnonymous]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignUp(string user, string password, string company)
        {

            bool existingCompany = _db.Accounts.Any(x => x.Name.ToLower() == company.ToLower());
            bool existingUser = _db.Users.Any(x => x.Name.ToLower() == user.ToLower());

            if (existingCompany || existingUser)
            {
                return View("Error", new Exception("Found and existing user or compnay name, please select a different username or company name"));
            }

            Guid accountId = Guid.NewGuid();
            Guid applicationId = Guid.NewGuid();

            _db.Accounts.Add(new Account() { ID = accountId, Name = company });
            _db.Users.Add(new User() { Account = accountId, ID = Guid.NewGuid(), Name = user, Password = password });
            _db.Applications.Add(new Application() { Account = accountId, ApiKey = Guid.NewGuid(), ID = Guid.NewGuid(), LastMessageDateTime = DateTime.MinValue, Name = "My First Appliation", MaxMessages = 25, Persist = false });


            Session.Add("AccountId", accountId);

            _db.SaveChanges();

            return View("RegistrationComplete");
        }

        public ActionResult AddApplication()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddApplication(string name)
        {
            Guid accountId = (Guid)Session["AccountId"];

            _db.Applications.Add(
                new Application()
                {
                    ID = Guid.NewGuid(),
                    Name = name,
                    Account = accountId,
                    ApiKey = Guid.NewGuid(),
                    LastMessageDateTime = DateTime.MinValue,
                    MaxMessages = 25,
                    Persist = false
                });

            _db.SaveChanges();

            return RedirectToAction("Account");
        }

    }
}
