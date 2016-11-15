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
    public class PlayersController : Controller
    {
        private PlayerRepository playerRepo;
        private RoleRepository roleRepo;
        private GameRepository gameRepo;
        private FineRepository fineRepo;
        private PlayerTeamDivisionRepository ptdRepo;

        public PlayersController()
        {
            this.playerRepo = new PlayerRepository(new TennisContext());
            this.roleRepo = new RoleRepository(new TennisContext());
            this.gameRepo = new GameRepository(new TennisContext());
            this.fineRepo = new FineRepository(new TennisContext());
            this.ptdRepo = new PlayerTeamDivisionRepository(new TennisContext());
        }

        // GET: Players
        public ActionResult Index()
        {
            return View(playerRepo.GetAll());
        }

        // GET: Players/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = playerRepo.GetByIdWithEverything(id.Value);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: Players/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PlayerNr,LastName,FirstName,Birthday,Gender,JoinYear,Street,HouseNr,ZipCode,City,PhoneNr,FederationNr")] Player player)
        {
            if (ModelState.IsValid)
            {
                playerRepo.Create(player);
                playerRepo.Save();
                return RedirectToAction("Index");
            }

            return View(player);
        }

        // GET: Players/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = playerRepo.GetByIdWithEverything(id.Value);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PlayerNr,LastName,FirstName,Birthday,Gender,JoinYear,Street,HouseNr,ZipCode,City,PhoneNr,FederationNr")] Player player)
        {
            string role_id_list_string = Request.Form["roles_list"];
            List<Role> role_list = new List<Role>();
            if (!string.IsNullOrEmpty(role_id_list_string))
            {
                string[] role_id_list = role_id_list_string.Split(',');
                foreach (string role_id in role_id_list)
                {
                    role_list.Add(roleRepo.GetById(int.Parse(role_id)));
                }
            }
            if (ModelState.IsValid)
            {
                playerRepo.Update(player);
                playerRepo.UpdateRoles(player.Id, role_list);
                playerRepo.Save();
                return RedirectToAction("Index");
            }
            return View(player);
        }

        public ActionResult AddTeamDivision(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.playerId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTeamDivision(int playerId, int divisionId, int teamId)
        {

            if (ModelState.IsValid)
            {
                PlayerTeamDivision ptd = new PlayerTeamDivision(playerId, teamId, divisionId);
                ptdRepo.Create(ptd);
                ptdRepo.Save();
                return RedirectToAction("Edit", new { id = playerId });
            }
            return View();
        }

        public ActionResult DeleteTeamDivision(int? playerId, int? divisionId, int? teamId)
        {
            if (teamId == null || playerId == null || divisionId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ptdRepo.Delete(playerId.Value, teamId.Value, divisionId.Value);
            ptdRepo.Save();
            return RedirectToAction("Edit", new { id = playerId });
        }

        // GET: Players/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = playerRepo.GetById(id.Value);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = playerRepo.GetByIdWithEverything(id);
            foreach (var item in player.Player1Games)
            {
                gameRepo.Delete(item.Id);
                gameRepo.Save();
            }
            foreach (var item in player.Player2Games)
            {
                gameRepo.Delete(item.Id);
                gameRepo.Save();
            }
           
            foreach (var item in player.Fines)
            {
                fineRepo.Delete(item.Id);
                fineRepo.Save();
            }
            playerRepo.Delete(id);
            try
            {
                playerRepo.Save();
            }
            catch (Exception e)
            {
                return RedirectToAction("Delete", new { id = id });
            }
            return RedirectToAction("Index");
        }

        // For select2 functionality
        public JsonResult FetchPlayers(string searchTerm)
        {
            if (searchTerm == null || searchTerm == "")
            {
                var players = playerRepo.GetAll();
                var json = from player in players
                           select new
                           {
                               text = player.NameNumber,
                               id = player.Id,
                           };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var players = playerRepo.GetFilteredByNameNumber(searchTerm);
                var json = from player in players
                           select new
                           {
                               text = player.NameNumber,
                               id = player.Id,
                           };
                return Json(json, JsonRequestBehavior.AllowGet);
            }
            
        }

        public ActionResult AddFine(int? playerId)
        {
            if (playerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.playerId = playerId;
            Fine fine = new Fine();
            fine.FineDate = DateTime.Today.Date;
            return View(fine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFine([Bind(Include = "Id,PlayerId,FineDate,Amount")] Fine fine)
        {

            if (ModelState.IsValid)
            {
                fineRepo.Create(fine);
                fineRepo.Save();
                return RedirectToAction("Edit", new { id = fine.PlayerId });
            }
            return View();
        }

        public ActionResult EditFine(int? playerId, int? fineId)
        {
            if (playerId == null || fineId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.playerId = playerId;
            Fine fine = fineRepo.GetByIdWithPlayer(fineId.Value);
            if (fine == null)
            {
                return HttpNotFound();
            }
            return View(fine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFine([Bind(Include = "Id,PlayerId,FineDate,Amount")] Fine fine)
        {

            if (ModelState.IsValid)
            {
                fineRepo.Update(fine);
                fineRepo.Save();
                return RedirectToAction("Edit", new { id = fine.PlayerId });
            }
            return View();
        }


        public ActionResult DeleteFine(int? fineId, int? playerId)
        {
            if (fineId == null || playerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fineRepo.Delete(fineId.Value);
            fineRepo.Save();
            return RedirectToAction("Edit", new { id = playerId });
        }

        protected override void Dispose(bool disposing)
        {
            playerRepo.Dispose();
            roleRepo.Dispose();
            gameRepo.Dispose();
            fineRepo.Dispose();
            ptdRepo.Dispose();
            base.Dispose(disposing);
        }
    }
}
