using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ZeroHunger.EF;
using ZeroHunger.Models;
using AutoMapper;
using Login = ZeroHunger.Models.Login;

namespace ZeroHunger.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login model)
        {
            var db = new ZeroHungerEntities();
            var user = (from u in db.Registrations where u.Username == model.UserName && u.Password == model.Password select u).SingleOrDefault();
            if (user != null)
            {
                Session["UserId"] = user.Id;
                Session["UserType"] = user.UserType;

                switch (user.UserType)
                {
                    case "Admin":
                        Session["Admin"] = "logged";
                        return RedirectToAction("Dashboard", "Admin");
                    case "Employee":
                        Session["Employee"] = "logged";
                        return RedirectToAction("Dashboard", "Employees");
                    case "Restaurant":
                        Session["Restaurant"] = "logged";
                        return RedirectToAction("Dashboard", "Restaurant");
                    default:
                        break;
                }
            }
            else
            {
                ViewBag.Msg = "Login Failed";
            }
            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(RegistrationClass model)
        {
            var db = new ZeroHungerEntities();
            var existingUser = db.Registrations.FirstOrDefault(u => u.Username == model.Username);

            if (existingUser == null)
            {
                var user = new Registration
                {
                    Username = model.Username,
                    Password = model.Password,
                    UserType = model.UserType
                };

                db.Registrations.Add(user);
                db.SaveChanges();

                
                var registrationId = user.Id;

                if (model.UserType.Equals("Employee"))
                {
                    var employee = new Employee
                    {
                        Name = model.Name,
                        Phone = model.Phone,
                        Email = model.Email,
                        RegistrationId = registrationId
                    };
                    db.Employees.Add(employee);
                }
                else if (model.UserType.Equals("Restaurant"))
                {
                    var restaurant = new Restaurant
                    {
                        SupplierName = model.Name,
                        ContactNumber = model.Phone,
                        Email = model.Email,
                        Name = "-",
                        Location = "-",
                        RegistrationId = registrationId
                    };
                    db.Restaurants.Add(restaurant);
                }

                db.SaveChanges();
                ViewBag.Msg = "Registration Successful";
            }
            else
            {
                ViewBag.Msg = "Username already exists";
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
