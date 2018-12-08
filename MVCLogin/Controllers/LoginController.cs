using MVCLogin.Models;
using System.Linq;
using System.Web.Mvc;

namespace MVCLogin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(User userModel)
        {
            using (LoginEntities db = new LoginEntities())
            {
                var userDetails = db.Users.Where(x => x.UserName == userModel.UserName
                                                 && x.PassWord == userModel.PassWord)
                                                 .FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Incorrect Username or Password";
                    return View("Index", userModel);
                }
                else
                {
                    Session["userID"] = userDetails.UserID;
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}