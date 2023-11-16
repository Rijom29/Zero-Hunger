using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.DTOs;
using ZeroHunger.EF;
using ZeroHunger.Models;

namespace ZeroHunger.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Dashboard()
        {
            int userId = (int)Session["UserId"];
            string userType = (string)Session["UserType"];
            var db = new ZeroHungerEntities();
            var pendingRequests = db.CollectRequests
            .Where(p => p.CollectionStatus == "Pending")
            .ToList();


            var restaurants = db.Restaurants.ToList();
            var employees = db.Employees.ToList();

            ViewBag.PendingRequestsCount = pendingRequests.Count;

            ViewBag.RestaurantsCount = restaurants.Count;
            ViewBag.EmployeesCount = employees.Count;
            ViewBag.PendingRequests = pendingRequests;
            return View();

        }
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(RegistrationClass model)
        {
            var db = new ZeroHungerEntities();
            var extUsername = (from u in db.Registrations where u.Username == model.Username select u).SingleOrDefault();
            if (extUsername == null)
            {
                var user = new Registration()
                {
                    Username = model.Username,
                    Password = model.Password,
                    UserType = model.UserType
                };
                db.Registrations.Add(user);
                db.SaveChanges();
                var registrationId = (from r in db.Registrations where r.Username == model.Username select r).SingleOrDefault();
                if (model.UserType.Equals("Employee"))
                {
                    var employee = new Employee()
                    {
                        Name = model.Name,
                        Phone = model.Phone,
                        Email = model.Email,
                        RegistrationId = registrationId.Id
                    };
                    db.Employees.Add(employee);
                }
                if (model.UserType.Equals("Admin"))
                {
                    var admin = new Admin()
                    {
                        Name = model.Name,
                        Phone = model.Phone,
                        Email = model.Email,
                        RegistrationId = registrationId.Id
                    };
                    db.Admins.Add(admin);
                }
                if (model.UserType.Equals("Restaurent"))
                {
                    var restaurents = new Restaurant()
                    {
                        SupplierName = model.Name,
                        ContactNumber = model.Phone,
                        Email = model.Email,
                        Name = "-",
                        Location = "-",
                        RegistrationId = registrationId.Id
                    };
                    db.Restaurants.Add(restaurents);
                }
                db.SaveChanges();
                ViewBag.msg = "Registration Successfull";
                return RedirectToAction("Dashboard", "Admin");
  
            }
            else
            {
                ViewBag.msg = "User Name exists";
                return View();
            }
           
        }



        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }

    }

}
