﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyHappyDays.Models;

namespace MyHappyDays.Controllers
{
    public class PaymentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Payments
        //public async Task<ActionResult> Index()
        //{
        //    var payments = db.Payments.Include(p => p.Enrolments);
        //    return View(await payments.ToListAsync());
        //}

        // GET: Payments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = await db.Payments.FindAsync(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            //ViewBag.EnrolmentID = new SelectList(db.Enrolments, "ID", "ID");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        //payment setup is outside the scope of this project
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AmountPaid,AmountDue,DateReceived,PayeeName")] Payment payment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var currentUser = User.Identity;
                    var currentID = User.Identity.GetUserId();
                    if (User.IsInRole("Club Manager"))
                    {
                        db.Payments.Add(payment);
                        await db.SaveChangesAsync();
                        return RedirectToAction("MyDashboard", "Clubs");
                    }
                    else
                    {
                        return RedirectToAction("MyDashboard", "Children");
                    }
                }

                //ViewBag.EnrolmentID = new SelectList(db.Enrolments, "ID", "ID", payment.EnrolmentID);
                return View(payment);
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Can't complete payment right now.  Try again, if the problem persists, check with your system administrator");
            }
            return View();
        }

        // GET: Payments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = await db.Payments.FindAsync(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.EnrolmentID = new SelectList(db.Enrolments, "ID", "ID", payment.ID);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EnrolmentID,ID,AmountReceived,AmountDue,DateReceived,PayeeName")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EnrolmentID = new SelectList(db.Enrolments, "ID", "ID", payment.ID);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = await db.Payments.FindAsync(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Payment payment = await db.Payments.FindAsync(id);
            db.Payments.Remove(payment);
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
