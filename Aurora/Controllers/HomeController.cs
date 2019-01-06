using Aurora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Aurora.Controllers
{
    public class HomeController : Controller
    {
        private DataModel db = new DataModel();
        public ActionResult Index()
        {
            List<Room> allRooms;
            using (db)
            {
                allRooms = db.Rooms.Include(c => c.CreatedBy).ToList();
            }
               return View(allRooms);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Support()
        {
            return View();
        }
    }
}