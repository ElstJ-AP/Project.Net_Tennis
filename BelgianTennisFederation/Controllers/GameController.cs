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
    public class GameController : Controller
    {
        private GameRepository gameRepo;

        public GameController()
        {
            var context = new TennisContext();
            this.gameRepo = new GameRepository(context);
        }

        // GET: Game
        public ActionResult Index()
        {
            return View(gameRepo.GetAllWithPlayers());
        }

        // GET: Game/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = gameRepo.GetByIdWithPlayers(id.Value);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Game/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Game/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Player1Id,Player2Id")] Game game)
        {
            int[] Player1ScoreArray = new int[5];
            int[] Player2ScoreArray = new int[5];
            for (int i = 1; i <= 5; i++)
            {
                string Player1Set = Request.Form["Player1Set"+i];
                if(!string.IsNullOrEmpty(Player1Set)){
                    Player1ScoreArray[i - 1] = int.Parse(Player1Set);
                }
                string Player2Set = Request.Form["Player2Set" + i];
                if (!string.IsNullOrEmpty(Player2Set))
                {
                    Player2ScoreArray[i - 1] = int.Parse(Player2Set);
                }
            }
            game.Player1ScoreArray = Player1ScoreArray;
            game.Player2ScoreArray = Player2ScoreArray;
            if (ModelState.IsValid)
            {
                 gameRepo.Create(game);
                 gameRepo.Save();
                 return RedirectToAction("Index");
            }
            return View(game);
        }

        // GET: Game/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = gameRepo.GetByIdWithPlayers(id.Value);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Game/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Player1Id,Player2Id")] Game game)
        {
            int[] Player1ScoreArray = new int[5];
            int[] Player2ScoreArray = new int[5];
            for (int i = 1; i <= 5; i++)
            {
                string Player1Set = Request.Form["Player1Set" + i];
                if (!string.IsNullOrEmpty(Player1Set))
                {
                    Player1ScoreArray[i - 1] = int.Parse(Player1Set);
                }
                string Player2Set = Request.Form["Player2Set" + i];
                if (!string.IsNullOrEmpty(Player2Set))
                {
                    Player2ScoreArray[i - 1] = int.Parse(Player2Set);
                }
            }
            game.Player1ScoreArray = Player1ScoreArray;
            game.Player2ScoreArray = Player2ScoreArray;
            if (ModelState.IsValid)
            {
                gameRepo.Update(game);
                gameRepo.Save();
                return RedirectToAction("Index");
            }
            return View(game);
        }

        // GET: Game/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = gameRepo.GetByIdWithPlayers(id.Value);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Game/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            gameRepo.Delete(id);
            gameRepo.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                gameRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
