using System.Data;
using System.Diagnostics;
using CutBook.DataAccess;
using CutBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace CutBook.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult ViewHomePage()
        {
            return View();
        }

        public IActionResult GetLoginForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Username, string password)
        {
            DB_Helper dbHelper = new DB_Helper();
            ViewModelFactory viewModelFactory = new ViewModelFactory(dbHelper);
            string userID = viewModelFactory.LoginUser(Username, password);
            if (userID == null)
            {
                ViewBag.LoginError = true;
                return View("GetLoginForm");
            }
            string realName = viewModelFactory.GetUserName(userID);
            bool isAdmin = viewModelFactory.IsUserAdmin(userID);

            HttpContext.Session.SetString("userID", userID);
            HttpContext.Session.SetString("UserName", realName);
            HttpContext.Session.SetString("IsAdmin", isAdmin ? "true" : "false");

            if (isAdmin)
            {
                return RedirectToAction("Appointments", "Admin");
            }

            return RedirectToAction("ViewAppointments", "User");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("ViewHomePage");
        }
        public IActionResult GetSignUpForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(string userID, string userTel, string userEmail, string firstName, string lastName, string userPassword, bool isAdmin)
        {
            DB_Helper dbHelper = new DB_Helper();
            ViewModelFactory viewModelFactory = new ViewModelFactory(dbHelper);
            if (viewModelFactory.IsUserExist(userID, userEmail))
            {
                ViewBag.Error = "A user with this ID number or email is already registered in the system.";
                return View("GetSignUpForm");
            }
            string fullName = $"{firstName} {lastName}";
            User newUser = new User(userID, userTel, userEmail, fullName, userPassword, isAdmin);
            string createdUserId = viewModelFactory.AddNewUser(newUser);
            if (createdUserId != null)
            {
                HttpContext.Session.SetString("userID", userID);
                HttpContext.Session.SetString("UserName", firstName);
                HttpContext.Session.SetString("IsAdmin", "false");
                return RedirectToAction("ViewHomePage");
            }
            return View("GetSignUpForm");
        }
    }
}