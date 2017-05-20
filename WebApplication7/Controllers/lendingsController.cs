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
    [Authorize(Roles = "A")]
    public class lendingsController : Controller
    {
        private libEntities db = new libEntities();

        // GET: lendings
        public ActionResult Index()
        {
            var lendings = db.lendings.Include(l => l.book).Include(l => l.member);
            return View(lendings.ToList());
        }

        // GET: lendings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lending lending = db.lendings.Find(id);
            if (lending == null)
            {
                return HttpNotFound();
            }
            return View(lending);
        }

        // GET: lendings/Create
        public ActionResult Create()
        {
            
            ViewBag.book_Id = new SelectList(db.books, "Id", "title");
            ViewBag.memberId = new SelectList(db.members, "Id", "firstname");
            return View();
        }

        // POST: lendings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,starttime,endtime,memberId,book_Id")] lending lending)
        {
            if (ModelState.IsValid)
            {
                db.lendings.Add(lending);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.book_Id = new SelectList(db.books, "Id", "isbn", lending.book_Id);
            ViewBag.memberId = new SelectList(db.members, "Id", "firstname", lending.memberId);
            return View(lending);
        }

        // GET: lendings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lending lending = db.lendings.Find(id);
            if (lending == null)
            {
                return HttpNotFound();
            }
            ViewBag.book_Id = new SelectList(db.books, "Id", "isbn", lending.book_Id);
            ViewBag.memberId = new SelectList(db.members, "Id", "firstname", lending.memberId);
            return View(lending);
        }

        // POST: lendings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,starttime,endtime,memberId,book_Id")] lending lending)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lending).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.book_Id = new SelectList(db.books, "Id", "isbn", lending.book_Id);
            ViewBag.memberId = new SelectList(db.members, "Id", "firstname", lending.memberId);
            return View(lending);
        }

        // GET: lendings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lending lending = db.lendings.Find(id);
            if (lending == null)
            {
                return HttpNotFound();
            }
            return View(lending);
        }

        // POST: lendings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            lending lending = db.lendings.Find(id);
            db.lendings.Remove(lending);
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
