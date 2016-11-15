using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TennisDomain.Classes;
using TennisDomain.DataModel;

namespace BelgianTennisFederation.Controllers
{
    public class RoleController : Controller
    {
        private RoleRepository roleRepo;
        private PlayerRepository playerRepo;

        public RoleController()
        {
            var context = new TennisContext();
            this.roleRepo = new RoleRepository(context);
            this.playerRepo = new PlayerRepository(context);
        }

        // GET: Role
        public ActionResult Index()
        {
            return View(roleRepo.GetAllWithPlayers());
        }

        // GET: Role/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = roleRepo.GetByIdWithPlayers(id.Value);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Role role)
        {
            string player_id_list_string = Request.Form["player_list"];
            if (!string.IsNullOrEmpty(player_id_list_string))
            {
                string[] player_id_list = player_id_list_string.Split(',');
                List<Player> player_list = new List<Player>();
                foreach (string player_id in player_id_list)
                {
                    player_list.Add(playerRepo.GetById(int.Parse(player_id)));
                }
                role.Players = player_list;
            }
            if (ModelState.IsValid)
            {
                roleRepo.Create(role);
                roleRepo.Save();
                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: Role/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = roleRepo.GetByIdWithPlayers(id.Value);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Role/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Role role)
        {
            string player_id_list_string = Request.Form["player_list"];
            List<Player> player_list = new List<Player>();
            if (!string.IsNullOrEmpty(player_id_list_string))
            {
                string[] player_id_list = player_id_list_string.Split(',');
                foreach (string player_id in player_id_list)
                {
                    player_list.Add(playerRepo.GetById(int.Parse(player_id)));
                }                
            }
            if (ModelState.IsValid)
            {
                roleRepo.Update(role);
                roleRepo.UpdatePlayers(role.Id, player_list);
                roleRepo.Save();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: Role/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = roleRepo.GetByIdWithPlayers(id.Value);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            roleRepo.Delete(id);
            roleRepo.Save();
            return RedirectToAction("Index");
        }

        // For select2 functionality
        public JsonResult FetchRoles(string searchTerm)
        {
            if (searchTerm == null || searchTerm == "")
            {
                var roles = roleRepo.GetAll();
                var json = from role in roles
                           select new
                           {
                               text = role.Name,
                               id = role.Id,
                           };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var roles = roleRepo.GetFilteredByName(searchTerm);
                var json = from role in roles
                           select new
                           {
                               text = role.Name,
                               id = role.Id,
                           };
                return Json(json, JsonRequestBehavior.AllowGet);
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                roleRepo.Dispose();
                playerRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
