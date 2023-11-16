using System;
using System.Linq;
using System.Web.Mvc;
using ZeroHunger.DTOs;
using ZeroHunger.EF;

namespace ZeroHunger.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly ZeroHungerEntities db = new ZeroHungerEntities();

        public ActionResult Dashboard()
        {
            int userId = (int)Session["UserId"];
            var extRestaurant = GetRestaurantById(userId);

            if (extRestaurant != null)
            {
                var pendingRequests = db.CollectRequests
                    .Where(p => p.CollectionStatus == "Pending" && p.RestaurantId == extRestaurant.Id)
                    .ToList();

                var acceptedRequests = db.CollectRequests
                    .Where(p => p.CollectionStatus == "Accepted" && p.RestaurantId == extRestaurant.Id)
                    .ToList();

                var myRequests = db.CollectRequests
                    .Where(cr => cr.RestaurantId == extRestaurant.Id)
                    .ToList();

                ViewBag.pendingcount = pendingRequests.Count;
                ViewBag.acceptedcount = acceptedRequests.Count;
                ViewBag.myRequests = myRequests;

                return View();
            }
            else
            {
                ViewBag.ErrorMessage = "Restaurant not found.";
                return View();
            }
        }

        public new ActionResult Profile()
        {
            int userId = (int)Session["UserId"];
            var extRestaurant = GetRestaurantById(userId);

            if (extRestaurant != null)
            {
                var restaurantDTO = new RestaurantDTO
                {
                    Id = extRestaurant.Id,
                    Name = extRestaurant.Name,
                    Location = extRestaurant.Location,
                    ContactNumber = extRestaurant.ContactNumber,
                    SupplierName = extRestaurant.SupplierName
                };

                return View(restaurantDTO);
            }
            else
            {
                ViewBag.ErrorMessage = "Restaurant not found.";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ProfileEdit()
        {
            int userId = (int)Session["UserId"];
            var extRestaurant = GetRestaurantById(userId);

            if (extRestaurant != null)
            {
                var restaurantDTO = new RestaurantDTO
                {
                    Id = extRestaurant.Id,
                    Name = extRestaurant.Name,
                    Location = extRestaurant.Location,
                    ContactNumber = extRestaurant.ContactNumber,
                    SupplierName = extRestaurant.SupplierName
                };

                return View(restaurantDTO);
            }
            else
            {
                ViewBag.ErrorMessage = "Restaurant not found.";
                return View();
            }
        }

        [HttpPost]
        public ActionResult ProfileEdit(RestaurantDTO model)
        {
            int userId = (int)Session["UserId"];
            var extRestaurant = GetRestaurantById(userId);

            if (extRestaurant != null)
            {
                extRestaurant.Name = model.Name;
                extRestaurant.Location = model.Location;
                extRestaurant.ContactNumber = model.ContactNumber;
                extRestaurant.SupplierName = model.SupplierName;

                db.SaveChanges();
                ViewBag.msg = "Successfully Saved";

                if (!Session["Redirect"].Equals(""))
                {
                    Session["Redirect"] = "";
                    return RedirectToAction("Dashboard", "Restaurant");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Restaurant not found.";
                return View();
            }
        }

        [HttpGet]
        public ActionResult SendFoodRequest()
        {
            int userId = (int)Session["UserId"];
            var extRestaurant = GetRestaurantById(userId);

            if (extRestaurant != null)
            {
                return View();
            }
            else
            {
                ViewBag.ErrorMessage = "Restaurant not found.";
                return View();
            }
        }

        [HttpPost]
        public ActionResult SendFoodRequest(CollectRequestDTO model)
        {
            int userId = (int)Session["UserId"];
            var extRestaurant = GetRestaurantById(userId);

            if (extRestaurant != null)
            {
                return AddFoodRequest(model, extRestaurant);
            }
            else
            {
                ViewBag.ErrorMessage = "Restaurant not found.";
                return View();
            }
        }
       
        private Restaurant GetRestaurantById(int userId)
        {
            return db.Restaurants
                .Where(r => r.RegistrationId == userId)
                .SingleOrDefault();
        }

        private ActionResult AddFoodRequest(CollectRequestDTO model, Restaurant extRestaurant)
        {
            if (ModelState.IsValid)
            {
                var collectRequest = new CollectRequest
                {
                    RestaurantId = extRestaurant.Id,
                    FoodType = model.FoodType,
                    Quantity = model.Quantity,
                    MaxPreservationTime = model.MaxPreservationTime,
                    CollectionEmployeeId = model.CollectionEmployeeId,
                    CollectionStatus = "Pending"
                };

                db.CollectRequests.Add(collectRequest);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Food request sent successfully.";
                return RedirectToAction("Dashboard", "Restaurant");
            }

            return View("SendFoodRequest", model);
        }
        public ActionResult LogOut()
        {
            return RedirectToAction("Login", "Home");
        }

    }
}
