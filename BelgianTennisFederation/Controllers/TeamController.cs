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
    public class TeamController : Controller
    {
        private TeamRepository teamRepo;
        private PlayerTeamDivisionRepository ptdRepo;

        public TeamController()
        {
            this.teamRepo = new TeamRepository(new TennisContext());
            this.ptdRepo = new PlayerTeamDivisionRepository(new TennisContext());
        }

        // GET: Team
        public ActionResult Index()
        {
            return View(teamRepo.GetAll());
        }

        // GET: Team/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = teamRepo.GetById(id.Value);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // GET: Team/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Team/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Team team)
        {
            if (ModelState.IsValid)
            {
                teamRepo.Create(team);
                teamRepo.Save();
                return RedirectToAction("Index");
            }

            return View(team);
        }

        // GET: Team/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = teamRepo.GetById(id.Value);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Team/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Team team)
        {
            if (ModelState.IsValid)
            {
                teamRepo.Update(team);
                teamRepo.Save();
                return RedirectToAction("Index");
            }
            return View(team);
        }

        public ActionResult AddPlayerDivision(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.teamId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPlayerDivision(int playerId, int divisionId, int teamId)
        {

            if (ModelState.IsValid)
            {
                PlayerTeamDivision ptd = new PlayerTeamDivision(playerId, teamId, divisionId);
                ptdRepo.Create(ptd);
                ptdRepo.Save();
                return RedirectToAction("Edit", new { id = teamId });
            }
            return View();
        }

        public ActionResult DeletePlayerDivision(int? playerId, int? divisionId, int? teamId)
        {
            if (teamId == null || playerId == null || divisionId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ptdRepo.Delete(playerId.Value, teamId.Value, divisionId.Value);
            ptdRepo.Save();
            return RedirectToAction("Edit", new { id = teamId });
        }

        // GET: Team/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = teamRepo.GetById(id.Value);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Team/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            teamRepo.Delete(id);
            teamRepo.Save();
            return RedirectToAction("Index");
        }

        public JsonResult FetchTeams(string searchTerm)
        {
            if (searchTerm == null || searchTerm == "")
            {
                var teams = teamRepo.GetAll();
                var json = from team in teams
                           select new
                           {
                               text = team.Name,
                               id = team.Id,
                           };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var teams = teamRepo.GetFilteredByName(searchTerm);
                var json = from team in teams
                           select new
                           {
                               text = team.Name,
                               id = team.Id,
                           };
                return Json(json, JsonRequestBehavior.AllowGet);
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                teamRepo.Dispose();
                ptdRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
