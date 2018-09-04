using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using System.Data.Entity;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        BookContext db = new BookContext();
        public ActionResult Index()
        { 
            return View(db.Books);
        }
        [HttpGet]
        public ActionResult Buy(int ID)
        {
            ViewBag.BookID = ID;
            return View();
        }
        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            db.Purchases.Add(purchase);
            db.SaveChanges();
            return "Спасибо " + purchase.Person + " За покупку";
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        [HttpGet]
        public ActionResult BookView(int? ID)
        {

            return View(db.Books.Find(ID));
        }
        [HttpGet]
        public ActionResult EditBook(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Book book = db.Books.Find(id);
            if (book != null)
            {
                return View(book);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult EditBook(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult CreateBook()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateBook(Book book)
        {
            if (book.Author != null && book.Price!=null && book.Name!=null)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
            
        }
        [HttpGet]
        public ActionResult DeleteBook(int? ID)
        {
            Book book = db.Books.Find(ID);
            if (book != null)
            {

                return View(book);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost, ActionName("DeleteBook")]
        public ActionResult DeleteConfirmed(int? ID)
        {
            Book book = db.Books.Find(ID);
            if (book != null)
            {
                db.Books.Remove(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}