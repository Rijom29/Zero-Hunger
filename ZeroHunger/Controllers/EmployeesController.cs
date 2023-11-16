using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.DTOs;
using ZeroHunger.EF;

namespace ZeroHunger.Controllers
{

    public class EmployeesController : Controller
    {
        
        public ActionResult Dashboard()
        {
            int userId = (int)Session["UserId"];
            string userType = (string)Session["UserType"];
            var db = new ZeroHungerEntities();
            var extEmployee = db.Employees.FirstOrDefault(e => e.RegistrationId == userId);

            var pendingRequests = db.CollectRequests
            .Where(p => p.CollectionStatus == "Pending" && p.Employee.RegistrationId == userId)
            .ToList();

            var acceptedRequests = db.CollectRequests
            .Where(p => p.CollectionStatus == "Accepted" && p.Employee.RegistrationId == userId)
            .ToList();

            var collectedRequests = db.CollectRequests
            .Where(p => p.CollectionStatus == "Collected" && p.Employee.RegistrationId == userId)
            .ToList();

            ViewBag.pendingcount = pendingRequests.Count;
            ViewBag.acceptedcount = acceptedRequests.Count;
            ViewBag.collectedcount = collectedRequests.Count;

            var myRequests = db.CollectRequests
            .Where(cr => cr.CollectionEmployeeId == extEmployee.Id)
            .ToList();

            ViewBag.myRequests = myRequests;

            return View();
        }
        public ActionResult ViewRequests()
        {
            var db = new ZeroHungerEntities();
            var extRequests = db.CollectRequests.Where(cr => cr.CollectionStatus == "Pending").ToList();

            var collectRequestsDTO = extRequests.Select(request => new CollectRequestDTO
            {
                Id = request.Id,
                RestaurantId = (int)request.RestaurantId,
               
                FoodType = request.FoodType,
                Quantity = request.Quantity,
                MaxPreservationTime = request.MaxPreservationTime,
                CollectionStatus = request.CollectionStatus,
               
            }).ToList();

            Session["dot"] = "OFF";

            return View(collectRequestsDTO);
        }

        public ActionResult AcceptedRequests()
        {
            int userId = (int)Session["UserId"];
            string userType = (string)Session["UserType"];
            var db = new ZeroHungerEntities();
            var extEmployee = db.Employees.FirstOrDefault(e => e.RegistrationId == userId);

            var extRequests = db.CollectRequests.Where(cr => cr.CollectionStatus != "Pending" && cr.CollectionEmployeeId == extEmployee.Id).ToList();

            var collectRequestsDTO = extRequests.Select(request => new CollectRequestDTO
            {
                Id = request.Id,
                RestaurantId = (int)request.RestaurantId,
               
                FoodType = request.FoodType,
                Quantity = request.Quantity,
                MaxPreservationTime = request.MaxPreservationTime,
                CollectionStatus = request.CollectionStatus,
               
            }).ToList();

            return View(collectRequestsDTO);
        }

        public ActionResult Accept(int id)
        {
            int userId = (int)Session["UserId"];
            string userType = (string)Session["UserType"];
            var db = new ZeroHungerEntities();
            var extEmployee = db.Employees.FirstOrDefault(e => e.RegistrationId == userId);
            var extRequest = db.CollectRequests.Find(id);

            extRequest.CollectionStatus = "Accepted";
            extRequest.CollectionEmployeeId = extEmployee.Id;

            db.SaveChanges();

            return RedirectToAction("Dashboard","Employees");
        }

        public ActionResult Cancel(int id)
        {
            var db = new ZeroHungerEntities();
            var extRequest = db.CollectRequests.Find(id);

            extRequest.CollectionStatus = "Pending";
            extRequest.CollectionEmployeeId = null;

            db.SaveChanges();

            return RedirectToAction("Dashboard", "Employees");
        }

        public ActionResult Delivered(int id)
        {
            var db = new ZeroHungerEntities();
            var extRequest = db.CollectRequests.Find(id);

            extRequest.CollectionStatus = "Delivered";

            db.SaveChanges();

            return RedirectToAction("Dashboard", "Employees");
        }

        public new ActionResult Profile()
        {
            int userId = (int)Session["UserId"];
            string userType = (string)Session["UserType"];
            var db = new ZeroHungerEntities();
            var extEmployee = db.Employees.FirstOrDefault(e => e.RegistrationId == userId);

            var employeeDTO = new EmployeeDTO
            {
                Id = extEmployee.Id,
                RegistrationId = extEmployee.RegistrationId,
                Name = extEmployee.Name,
                Phone = extEmployee.Phone,
                Email = extEmployee.Email
            };

            return View(employeeDTO);
        }

        [HttpGet]
        public ActionResult ProfileEdit()
        {
            int userId = (int)Session["UserId"];
            string userType = (string)Session["UserType"];
            var db = new ZeroHungerEntities();
            var extEmployee = db.Employees.FirstOrDefault(e => e.RegistrationId == userId);

            var employeeDTO = new EmployeeDTO
            {
                Id = extEmployee.Id,
                RegistrationId = extEmployee.RegistrationId,
                Name = extEmployee.Name,
                Phone = extEmployee.Phone,
                Email = extEmployee.Email
            };

            return View(employeeDTO);
        }

        [HttpPost]
        public ActionResult ProfileEdit(EmployeeDTO model)
        {
            int userId = (int)Session["UserId"];
            string userType = (string)Session["UserType"];
            var db = new ZeroHungerEntities();
            var extEmployee = db.Employees.FirstOrDefault(e => e.RegistrationId == userId);

            extEmployee.Name = model.Name;
            extEmployee.Phone = model.Phone;
            extEmployee.Email = model.Email;

            db.SaveChanges();

            ViewBag.msg = "Successfully Saved";

            return View();
        }


        public ActionResult LogOut()
        {
            return RedirectToAction("Login", "Home");
        }
    }
}
