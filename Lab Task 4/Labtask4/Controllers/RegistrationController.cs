using Labtask4.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Labtask4.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult showCourses()
        {
            var db = new PreregistrationEntities();          

            return View(db.Courses.ToList());           
        }
        [HttpGet]
        public ActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCourse(Cours cr)
        {
            var db = new PreregistrationEntities();
            db.Courses.Add(cr);
            db.SaveChanges();            
            return RedirectToAction("showCourses");
        }

        [HttpGet]
        public ActionResult EditCourse(int id)
        {
            var db = new PreregistrationEntities();
            var existingCourse = (from cr in db.Students
                                    where cr.Id == id
                                    select cr).SingleOrDefault();
            return View(existingCourse);            
        }

        [HttpPost]
        public ActionResult EditCourse(Cours c)
        {
            var db = new PreregistrationEntities();
            var existingCourse = (from cr in db.Courses
                                    where cr.Id == c.Id
                                        select cr).SingleOrDefault();
            existingCourse.Name = c.Name;
            existingCourse.PreReq = c.PreReq;
            db.SaveChanges();
            return RedirectToAction("showCourses");
        }

    }
}