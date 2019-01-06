using Aurora.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Aurora.Controllers
{
    [Authorize]
    public class RoomController : Controller
    {
        private DataModel db = new DataModel();
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Room
        public ActionResult Index()
        {
            return View();
        }

        // GET: Room/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Room/Create
        public ActionResult Create()
        {
            if(!Request.IsAuthenticated)
            {
                TempData["actionStatus"] = "success";
                TempData["actionMessage"] = "Please create a user or login";
                return RedirectToAction("Index");
            }
            return View();
        }

        // POST: Room/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateRoomViewModel createRoomViewModel)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    using (db)
                    {
                        String username = User.Identity.Name;
                        ApplicationUser user = await UserManager.FindByNameAsync(username);
                        Room newRoom = new Room() {floor=createRoomViewModel.Number, created_by=user.Id,created_at=DateTime.Now };
                        db.Rooms.Add(newRoom);
                        //ActionLog action = new ActionLog() { UserId=user.Id,ModelName="Room", ActionType="Create" ,ActionPayload=JsonConvert.SerializeObject(newRoom,Formatting.Indented)};
                        //db.ActionLogs.Add(action);
                        await db.SaveChangesAsync();
                        TempData["actionStatus"] = "success";
                        TempData["actionMessage"] = "Room created successfully";
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    TempData["actionStatus"] = "danger";
                    TempData["actionMessage"] = "Model State is invalid";
                }
                return View(createRoomViewModel);
        }
            catch(Exception e)
            {
                TempData["actionStatus"] = "danger";
                TempData["actionMessage"] = e.Message;
                return View(createRoomViewModel);
    }
}

        // GET: Room/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Room/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, Room room)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    using (db)
                    {
                        String username = User.Identity.Name;
                        ApplicationUser user = await UserManager.FindByNameAsync(username);
                        Room roomToUpdate = db.Rooms.Find(id);
                        roomToUpdate.floor = room.floor;
                        roomToUpdate.updated_at = DateTime.Now;
                        await db.SaveChangesAsync();
                    }
                    TempData["actionStatus"] = "success";
                    TempData["actionMessage"] = "Room floor updated successfully";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(room);
                }  
            }
            catch
            {
                return View();
            }
        }

        // GET: Room/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Room/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, Room room)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    using (db)
                    {
                        String username = User.Identity.Name;
                        ApplicationUser user = await UserManager.FindByNameAsync(username);
                        Room roomToDelete = db.Rooms.Find(id);
                        db.Rooms.Remove(roomToDelete);
                        await db.SaveChangesAsync();
                    }
                    TempData["actionStatus"] = "success";
                    TempData["actionMessage"] = "Room deleted successfully";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(room);
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
