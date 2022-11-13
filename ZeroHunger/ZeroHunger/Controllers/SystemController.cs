using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.DB;

namespace ZeroHunger.Controllers
{   
    public class SystemController : Controller
    {
        //Login
        [HttpGet]                
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            var type = form["Type"].ToString();
            var name = form["Name"].ToString();
            var Pass = form["Pass"].ToString();
            
            var db = new ZeroHungerEntities();           
            switch (type)
            {
                case "NGO":                                        
                            var ext = (from cr in db.NGOes
                                       where cr.NGO_Name == name && cr.NGO_Password == Pass
                                       select cr).Count();

                            if (ext == 1)
                            {                        
                                return RedirectToAction("NGO_Dashboard");
                            }
                            else
                            {
                                return RedirectToAction("Login");
                            }    
                            
                case "Restaurant":                    
                                    var res = (from cr in db.Restaurants
                                                where cr.Restaurant_Name == name && cr.Restaurant_Pass== Pass
                                                   select cr).Count();
                                    if (res == 1)
                                    {
                                        return RedirectToAction("Restaurant_Dashboard");
                                    }
                                    else
                                    {
                                        return RedirectToAction("Login");
                                    }
                case "Employee":
                                    var employe_existence = (from cr in db.Employees
                                                                where cr.Employee_Name == name && cr.Employee_Pass == Pass
                                                                    select cr);
                    
                                    Session["id"] = (from cr in db.Employees
                                                        where cr.Employee_Name == name && cr.Employee_Pass == Pass
                                                            select cr.Employee_Id).SingleOrDefault();

                                    var count = employe_existence.Count();
                                    if (count == 1)
                                    {
                                        Session["check"] = true;
                                        return RedirectToAction("Employee_Dashboard");
                                    }
                                    else
                                    {   
                                        return RedirectToAction("Login");
                                    }
                default:
                    return View();                    
            }
        }        

        // NGO System
        [HttpGet]
        public ActionResult NGO_Dashboard()
        {
            return View();
        }

        public ActionResult EmployeesList()
        {
            var db = new ZeroHungerEntities();
            var employees = db.Employees.ToList();
            return View(employees);
        }

        [HttpGet]
        public ActionResult EnrollEmployee()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EnrollEmployee(Employee emp)
        {
            var db = new ZeroHungerEntities();
            db.Employees.Add(emp);
            db.SaveChanges();
            return RedirectToAction("NGO_Dashboard");            
        }

        [HttpGet]
        public  ActionResult RequestHistory()
        {
            var db = new ZeroHungerEntities();            
            var ext = (from cr in db.Collect_Request
                        where cr.Status.Equals("Distribution Complete")
                            select cr).ToList();
            return View(ext);
        }
        //********************************

        [HttpGet]
        public ActionResult RestaurantList()
        {
            var db = new ZeroHungerEntities();
            var restaurants = db.Restaurants.ToList();
            return View(restaurants);
        }
        [HttpGet]
        public ActionResult AddRestaurant()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRestaurant(Restaurant res)
        {
            var db = new ZeroHungerEntities();
            db.Restaurants.Add(res);
            db.SaveChanges();
            return RedirectToAction("NGO_Dashboard");
        }

        //Request List

        [HttpGet]
        public ActionResult ViewCollectRequest()
        {
            var db = new ZeroHungerEntities();
            var collect_Requests = db.Collect_Request.ToList();
            return View(collect_Requests);
        }

        //Update Status of collect request
        [HttpGet]
        public ActionResult UpdateStatus(int id)
        {
            var db = new ZeroHungerEntities();
            var ext = (from cr in db.Collect_Request
                            where cr.Request_Id== id
                                select cr).SingleOrDefault();
            return View(ext);
        }
        [HttpPost]
        public ActionResult UpdateStatus(Collect_Request us)
        {
            var db = new ZeroHungerEntities();
            var ext = (from cr in db.Collect_Request
                        where cr.Request_Id == us.Request_Id
                            select cr).SingleOrDefault();           
            ext.Status = us.Status;
            db.SaveChanges();
            return RedirectToAction("ViewCollectRequest");
        }

        //Assign an Employee to request
        [HttpGet]
        public ActionResult AssignEmployee(int id)
        {
            var db = new ZeroHungerEntities();
            var ext = (from cr in db.Collect_Request
                            where cr.Request_Id== id
                                select cr).SingleOrDefault();
            return View(ext);
        }
        [HttpPost]
        public ActionResult AssignEmployee(Collect_Request cs)
        {
            var db = new ZeroHungerEntities();
            var ext = (from cr in db.Collect_Request
                            where cr.Request_Id == cs.Request_Id
                                select cr).SingleOrDefault();
            
            ext.Employee_Id = cs.Employee_Id;
            db.SaveChanges();
            return RedirectToAction("ViewCollectRequest");

        }


        //************Restaurant******

        [HttpGet]
        public ActionResult Restaurant_Dashboard()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateCollectRequest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCollectRequest(Collect_Request cr)
        {
            var db = new ZeroHungerEntities();
            db.Collect_Request.Add(cr);
            db.SaveChanges();
            return RedirectToAction("Restaurant_Dashboard");
        }


        //Employee
        public ActionResult Employee_Dashboard()
        {
            return View();
        }

        public ActionResult PendingFoodCollection()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                var db = new ZeroHungerEntities();
                var employee_id = Session["id"];
                var ext = (from cr in db.Collect_Request
                            where cr.Status == "Accepted"
                                select cr).ToList();
                return View(ext);
            }
            
            
        }
        

        [HttpGet]
        public ActionResult Completeness_Status(int id)
        {
            var db = new ZeroHungerEntities();
            var ext = (from cr in db.Collect_Request
                       where cr.Request_Id == id
                       select cr).SingleOrDefault();
            return View(ext);
        }
        [HttpPost]
        public ActionResult Completeness_Status(Collect_Request us)
        {
            var db = new ZeroHungerEntities();
            var ext = (from cr in db.Collect_Request
                       where cr.Request_Id == us.Request_Id
                       select cr).SingleOrDefault();
            ext.Status = "Distribution Complete";
            db.SaveChanges();

            return RedirectToAction("PendingFoodCollection");
        }
    }
}