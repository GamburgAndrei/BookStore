using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class PlayersController : Controller
    {
        SoccerContext db = new SoccerContext();
        // GET: Players
        public ActionResult Index()
        {
            var players = db.Players.Include(m => m.team);
            return View(players.ToList());
        }
        public ActionResult ViewTeam()
        {
           
            return View(db.Teams);
        }
        public ActionResult TeamDetails(int? ID)
        {
            if (ID == null)
            {
                return HttpNotFound();
            }
            Team team = db.Teams.Include(t => t.Players).FirstOrDefault(t => t.ID == ID);
            if (team != null)
            {
                return View(team);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpGet]
        public ActionResult CreatePlayer()
        {
            SelectList team = new SelectList(db.Teams, "ID", "Name");
            ViewBag.Team = team;
            return View();
        }
        [HttpPost]
        public ActionResult CreatePlayer(Player player)
        {
            db.Players.Add(player);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditPlayer(int? ID)
        {
            if (ID == null)
            {
                return HttpNotFound();
            }
            Player player = db.Players.Find(ID);
            if (player == null)
            {
                return HttpNotFound();
            }
            else
            {
                SelectList team = new SelectList(db.Teams, "ID", "Name");
                ViewBag.Team = team;
                return View(player);
            }
        }
        [HttpPost]
        public ActionResult EditPlayer(Player player)
        {
            db.Entry(player).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}