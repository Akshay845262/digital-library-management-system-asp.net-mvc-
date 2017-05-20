using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class booksController : Controller
    {
        private libEntities db = new libEntities();

       
        [HttpGet]// GET: books
        public ActionResult Index()
        {
            var books = db.books.Include(b => b.category);
            return View(books.ToList());
        }

        [HttpPost]
        public ActionResult Index(string NameSearch)

        {

            var books = db.books.Include(b => b.category);

            if (NameSearch != null && NameSearch != " ")

            {
                 books = db.books.Where(b => b.title.Contains(NameSearch));



                  }  
        



                
           
                    
             //books = db.books.Include(b => b.category);
            return View(books.ToList());
        }


        // GET: books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [Authorize(Roles = "A")]
        // GET: books/Create
        public ActionResult Create()
        {
            ViewBag.category_Id = new SelectList(db.categories, "Id", "catname");
            return View();
        }

        // POST: books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( book book)
        {
            if (ModelState.IsValid)
            {
                db.books.Add(book);
                db.SaveChanges();
          //      book.file.SaveAs(Server.MapPath("~/IMG/") + book.Id + ".jpj");
                return RedirectToAction("Index");
            }

            ViewBag.category_Id = new SelectList(db.categories, "Id", "catname", book.category_Id);
            return View(book);
        }

        // GET: books/Edit/5
        [Authorize(Roles = "A")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_Id = new SelectList(db.categories, "Id", "catname", book.category_Id);
            return View(book);
        }

        // POST: books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,isbn,title,price,author,category_Id")] book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category_Id = new SelectList(db.categories, "Id", "catname", book.category_Id);
            return View(book);
        }

        // GET: books/Delete/5
        [Authorize(Roles = "A")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            book book = db.books.Find(id);
            db.books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
