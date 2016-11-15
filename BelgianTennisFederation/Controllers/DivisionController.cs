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
    public class DivisionController : Controller
    {
        private DivisionRepository divisionRepo;
        private PlayerTeamDivisionRepository ptdRepo;

        public DivisionController()
        {
            this.divisionRepo = new DivisionRepository(new TennisContext());
            this.ptdRepo = new PlayerTeamDivisionRepository(new TennisContext());
        }

        // GET: Division
        public ActionResult Index()
        {
            return View(divisionRepo.GetAll());
        }

        // GET: Division/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Division division = divisionRepo.GetById(id.Value);
            if (division == null)
            {
                return HttpNotFound();
            }
            return View(division);
        }

        // GET: Division/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Division/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Division division)
        {
            if (ModelState.IsValid)
            {
                divisionRepo.Create(division);
                divisionRepo.Save();
                return RedirectToAction("Index");
            }

            return View(division);
        }

        // GET: Division/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Division division = divisionRepo.GetById(id.Value);
            if (division == null)
            {
                return HttpNotFound();
            }
            return View(division);
        }

        // POST: Division/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Division division)
        {
            if (ModelState.IsValid)
            {
                divisionRepo.Update(division);
                divisionRepo.Save();
                return RedirectToAction("Index");
            }
            return View(division);
        }

        public ActionResult AddPlayerTeam(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.divisionId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPlayerTeam(int playerId, int divisionId, int teamId)
        {

            if (ModelState.IsValid)
            {
                PlayerTeamDivision ptd = new PlayerTeamDivision(playerId, teamId, divisionId);
                ptdRepo.Create(ptd);
                ptdRepo.Save();
                return RedirectToAction("Edit", new { id = divisionId });
            }
            return View();
        }

        public ActionResult DeletePlayerTeam(int? playerId, int? divisionId, int? teamId)
        {
            if (teamId == null || playerId == null || divisionId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ptdRepo.Delete(playerId.Value, teamId.Value, divisionId.Value);
            ptdRepo.Save();
            return RedirectToAction("Edit", new { id = divisionId });
        }

        // GET: Division/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Division division = divisionRepo.GetById(id.Value);
            if (division == null)
            {
                return HttpNotFound();
            }
            return View(division);
        }

        // POST: Division/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            divisionRepo.Delete(id);
            divisionRepo.Save();
            return RedirectToAction("Index");
        }

        public JsonResult FetchDivisions(string searchTerm)
        {
            if (searchTerm == null || searchTerm == "")
            {
                var divisions = divisionRepo.GetAll();
                var json = from div in divisions
                           select new
                           {
                               text = div.Name,
                               id = div.Id,
                           };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var divisions = divisionRepo.GetFilteredByName(searchTerm);
                var json = from div in divisions
                           select new
                           {
                               text = div.Name,
                               id = div.Id,
                           };
                return Json(json, JsonRequestBehavior.AllowGet);
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                divisionRepo.Dispose();
                ptdRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
