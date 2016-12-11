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

        //method to fill the dropdown list with clubs
        private void PopulateClubDropDownList(object selectedClub = null)
        {
            var clubsQuery = from c in db.Clubs
                             orderby c.ClubName
                             select c;

            ViewBag.ClubId = new SelectList(clubsQuery, "ID", "ClubName", selectedClub);

        }

        private void PopulateInstructorDropDownList(object selectedInstructor = null)
        {

            var instructorsQuery = from i in db.Instructors //where i.ClubID == clubID
                                   orderby i.InstructorLastName
                                   select i;
            ViewBag.InstructorID = new SelectList(instructorsQuery, "ID", "InstructorLastName", selectedInstructor);
        }

        //GET: Activities
        //public async Task<ActionResult> Index()
        //{
        //    var activities = db.Activities.Include(a => a.Club).Include(a => a.Instructor);
        //    return View(await activities.ToListAsync());
        //}
        [AllowAnonymous]
        //all activities and dropdown list to search by club
        public ActionResult Index(int? SelectedClub)
        {
            var clubs = db.Clubs.OrderBy(c => c.ClubName).ToList();
            ViewBag.SelectedClub = new SelectList(clubs, "ID", "ClubName", SelectedClub);
            int cluID = SelectedClub.GetValueOrDefault();

            IQueryable<Activity> activities = db.Activities.
                Where(a => !SelectedClub.HasValue || a.ClubID == cluID).
                Include(c => c.Club);
            var queryClubsActivitySearch = activities.ToString();
            return View(activities.ToList());

        }


        ////allow all visitors search activities
        //public ActionResult SearchAllActivities()
        //{
        //    var viewModel = new ActivitiesData();

        //    viewModel.Activities = db.Activities.
        //        Include(a => a.Club).
        //        Include(a => a.Enrolments.Select(c => c.Child)).
        //        Include(a => a.Instructor).
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
        //returning a blank create form and calling the populate clubs and instructors method to fill the dropdown menu 
        [Authorize(Roles = "Club Manager, Admin")]
        public ActionResult Create()
        {
            //ViewBag.ClubID = new SelectList(db.Clubs, "ID", "FirstName");
            //ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "InstructorFirstName");
            PopulateClubDropDownList();
            PopulateInstructorDropDownList();

            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Club Manager, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "NameOfActivity,MaxCapacity,AgeGroup,ActivityType,PriceOfActivity,ActivityCourseStartDate,ActivityCourseEndDate,Day,ClassTime, ClubID, InstructorID")] Activity activity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Activities.Add(activity);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes at the moment.  Try Again, if the problem continues see your administrator");
            }

            PopulateClubDropDownList(activity.ClubID);
            PopulateInstructorDropDownList(activity.InstructorID);
            
            return View(activity);
        }

        // GET: Activities/Edit/5
        [Authorize(Roles = "Club Manager, Admin")]
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

            PopulateClubDropDownList(activity.ClubID);
            PopulateInstructorDropDownList(activity.InstructorID);
           
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,NameOfActivity,MaxCapacity,AgeGroup,ActivityType,PriceOfActivity,ActivityCourseStartDate,ActivityCourseEndDate,Day,ClassTime,ClubID,InstructorID")] Activity activity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(activity).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes at the moment.  Try Again, if the problem continues see your administrator");
            }

            PopulateClubDropDownList(activity.ClubID);
            PopulateInstructorDropDownList(activity.InstructorID);

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
