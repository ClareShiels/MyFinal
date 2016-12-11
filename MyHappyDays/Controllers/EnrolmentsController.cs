using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Web.Mvc;
using MyHappyDays.Models;
using System.Data.Entity.Infrastructure;

namespace MyHappyDays.Controllers
{
    public class EnrolmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //populate drop down list with activities
        private void PopulateActivityDropDownList(object selectedActivity = null)
        {
            var activityQuery = from a in db.Activities
                                orderby a.NameOfActivity
                                select a;

            ViewBag.ActivityId = new SelectList(activityQuery, "ID", "NameOfActivity", selectedActivity);

        }

        // GET: Enrolments
        public async Task<ActionResult> Index()
        {
            var enrolments = db.Enrolments.Include(e => e.Activity).Include(e => e.Child)/*.Include(e => e.Payment)*/;
            return View(await enrolments.ToListAsync());
        }

        // GET: Enrolments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrolment enrolment = await db.Enrolments.FindAsync(id);
            if (enrolment == null)
            {
                return HttpNotFound();
            }
            return View(enrolment);
        }

        // GET: Enrolments/Create
        public ActionResult Create()
        {
            
            PopulateActivityDropDownList();
            
            //ViewBag.PaymentID = new SelectList(db.Payments, "ID", "PayeeName");
            return View();
        }

        // POST: Enrolments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,PaymentReceived,PaymentDue,ChildID,ActivityID")] Enrolment enrolment)
        {
            var currentUserId = User.Identity.GetUserId();
            var myChildren = db.Children.
                Where(c => c.UserID == currentUserId);
           
            //ViewBag.ActivityID = new SelectList(db.Activities, "ID", "NameOfActivity");
            try
            {
                if (ModelState.IsValid)
                {
                    db.Enrolments.Add(enrolment);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes at the moment.  Try Again, if the problem continues see your administrator");
            }
            //ViewBag.ActivityID = new SelectList(db.Activities, "ID", "NameOfActivity", enrolment.ActivityID);
            PopulateActivityDropDownList(enrolment.ActivityID);
            ViewBag.ChildID = new SelectList(myChildren, "ID", "FirstName", currentUserId);
            //ViewBag.ChildID = new SelectList(db.Children, "ID", "FirstName", enrolment.ChildID);
            //ViewBag.PaymentID = new SelectList(db.Payments, "ID", "PayeeName", enrolment.PaymentID);
            return View(enrolment);
        }

        // GET: Enrolments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrolment enrolment = await db.Enrolments.FindAsync(id);
            if (enrolment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityID = new SelectList(db.Activities, "ID", "NameOfActivity", enrolment.ActivityID);
            ViewBag.ChildID = new SelectList(db.Children, "ID", "FirstName", enrolment.ChildID);
            //ViewBag.PaymentID = new SelectList(db.Payments, "ID", "PayeeName", enrolment.PaymentID);
            return View(enrolment);
        }

        // POST: Enrolments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,PaymentReceived,PaymentDue,ChildID,ActivityID,PaymentID")] Enrolment enrolment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrolment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityID = new SelectList(db.Activities, "ID", "NameOfActivity", enrolment.ActivityID);
            ViewBag.ChildID = new SelectList(db.Children, "ID", "FirstName", enrolment.ChildID);
            //ViewBag.PaymentID = new SelectList(db.Payments, "ID", "PayeeName", enrolment.PaymentID);
            return View(enrolment);
        }

        // GET: Enrolments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrolment enrolment = await db.Enrolments.FindAsync(id);
            if (enrolment == null)
            {
                return HttpNotFound();
            }
            return View(enrolment);
        }

        // POST: Enrolments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Enrolment enrolment = await db.Enrolments.FindAsync(id);
            db.Enrolments.Remove(enrolment);
            await db.SaveChangesAsync();
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
