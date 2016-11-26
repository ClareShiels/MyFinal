using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyHappyDays.Models;
using MyHappyDays.DAL;
using MyHappyDays.ViewModels;

namespace MyHappyDays.Controllers
{
    public class ActivitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //GET: Activities
        //public async Task<ActionResult> Index()
        //{
        //    var activities = db.Activities.Include(a => a.Club).Include(a => a.Instructor);
        //    return View(await activities.ToListAsync());
        //}

        //trying Sat 26/11/16 display activities and their club
        public ActionResult Index(int? SelectedClub)
        {
            var clubs = db.Clubs.OrderBy(c => c.ClubName).ToList();
            ViewBag.SelectedClub = new SelectList(clubs, "ID", "ClubName", SelectedClub);
            int cluID = SelectedClub.GetValueOrDefault();

            IQueryable<Activity> activities = db.Activities.
                Where(a => !SelectedClub.HasValue || a.ClubID == cluID).
                OrderBy(c => c.ID).
                Include(c => c.Club);
            var sql = activities.ToString();
            return View(activities.ToList());

        }


        //allow all visitors search activities
        //public ActionResult SearchAllActivities(int? id)
        //{
        //    var viewModel = new ActivitiesData();

        //    viewModel.Activities = db.Activities.
        //        Include(a => a.Club).
        //        Include(a => a.Enrolments.Select(c => c.Child)).
        //        OrderBy(c => c.NameOfActivity);


        //    //if (id != null)
        //    //{
        //    //    ViewBag.ActivityID = id.Value;
        //    //    viewModel.Clubs = viewModel.Activities.
        //    //        Where(a => a.ID == id.Value).cl;
                   
        //    //}



        //    return View(viewModel);
        //}

        // GET: Activities/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = await db.Activities.FindAsync(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activities/Create
        public ActionResult Create()
        {
            ViewBag.ClubID = new SelectList(db.Clubs, "ID", "FirstName");
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "InstructorFirstName");
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,NameOfActivity,MaxCapacity,AgeGroup,ActivityType,PriceOfActivity,ActivityCourseStartDate,ActivityCourseEndDate,Day,ClassTime")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Activities.Add(activity);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ClubID = new SelectList(db.Clubs, "ID", "FirstName", activity.ClubID);
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "InstructorFirstName", activity.InstructorID);
            return View(activity);
        }

        // GET: Activities/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = await db.Activities.FindAsync(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClubID = new SelectList(db.Clubs, "ID", "FirstName", activity.ClubID);
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "InstructorFirstName", activity.InstructorID);
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,NameOfActivity,MaxCapacity,AgeGroup,ActivityType,PriceOfActivity,ActivityCourseStartDate,ActivityCourseEndDate,Day,ClassTime,ClubID,InstructorID")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ClubID = new SelectList(db.Clubs, "ID", "FirstName", activity.ClubID);
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "InstructorFirstName", activity.InstructorID);
            return View(activity);
        }

        // GET: Activities/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = await db.Activities.FindAsync(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Activity activity = await db.Activities.FindAsync(id);
            db.Activities.Remove(activity);
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
