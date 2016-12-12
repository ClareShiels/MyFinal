using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyHappyDays.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyHappyDays.ViewModels;

namespace MyHappyDays.Controllers
{
    public class ClubsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Clubs
        public async Task<ActionResult> Index()
        {
            var clubs = db.Clubs.Include(c => c.User);
            return View(await clubs.ToListAsync());
        }

        // GET: Clubs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club club = await db.Clubs.FindAsync(id);
            if (club == null)
            {
                return HttpNotFound();
            }
            return View(club);
        }

        //trying to return children in each club by passing in the userId
        //GET: all children registered in specific club
        [Authorize(Roles = "Club Manager, Admin")]
        public ActionResult ClubsKids(string sortOrder, string searchString, int clubId)
        {
            //var currentClubId = clubId;
            //viewbag variables used to allow the view to configure the column heading hyperlinks with appropriate query string values
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DOBSort = sortOrder == "DOB" ? "date_desc" : "DOB";
            var children = from c in db.Children
                           where c.ID == clubId    //currentClubId
                           select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                children = children.Where(c => c.ChildLastName.ToUpper().Contains(searchString.ToUpper()) ||
                                               c.ChildFirstName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    children = children.OrderByDescending(c => c.ChildLastName);
                    break;
                case "DOB":
                    children = children.OrderBy(c => c.DOB);
                    break;
                case "date_desc":
                    children = children.OrderByDescending(c => c.DOB);
                    break;
                default:
                    children = children.OrderBy(c => c.ChildLastName);
                    break;
            }
            return View(children.ToList());
        }

        //getting all data for each club for the viewmodel passing in the optional clubID value and the optional activityID value
        [HttpGet]
        public ActionResult MyDashboard(int? id, int? activityId)
        {
            //getting id of user
            var currentID = User.Identity.GetUserId();
    
            //creating new viewmodel
            var viewModel = new ClubData();

            //filling viewmodel with it's registered activities
            viewModel.Activities = db.Activities.
                Include(c => c.Club.Instructors).
                Include(c => c.Enrolments).
                Where(c => c.Club.UserID == currentID ).    
                OrderBy(c => c.NameOfActivity);
            
           viewModel.Clubs = db.Clubs.Where(c => c.UserID == currentID);

            if (id != null)
            {
                ViewBag.ClubID = id.Value;

                viewModel.Activities = viewModel.Clubs.
                    Where(c => c.ID == id.Value).Single().Activities;
                
            }

            if (activityId != null)
            {
                ViewBag.ActivityID = activityId.Value;

                viewModel.Enrolments = viewModel.Activities.
                    Where(a => a.ID == activityId.Value).Single().Enrolments;
            }

            return View(viewModel);
        }


        //GET: Clubs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clubs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ClubName,AddressLine1,AddressLine2,County,EirCode,ClubEmail,ContactPhNo,FirstName,LastName")] Club club)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var userId = User.Identity.GetUserId();
                    club.UserID = userId;
                    db.Clubs.Add(club);
                    await db.SaveChangesAsync();

                    return RedirectToAction("MyDashboard");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Can't save changes right now.  Try again, if the problem persists, check with your system administrator");
            }
            return View(club);

            
        }

        // GET: Clubs/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Club club = await db.Clubs.FindAsync(id);
        //    if (club == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.UserID = new SelectList(db.ApplicationUsers, "Id", "Email", club.UserID);
        //    return View(club);
        //}

        // POST: Clubs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "ID,FirstName,LastName,ContactPhNo,ClubEmail,ClubName,AddressLine1,AddressLine2,County,EirCode,UserID")] Club club)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(club).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.UserID = new SelectList(db.ApplicationUsers, "Id", "Email", club.UserID);
        //    return View(club);
        //}

        // GET: Clubs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club club = await db.Clubs.FindAsync(id);
            if (club == null)
            {
                return HttpNotFound();
            }
            return View(club);
        }

        // POST: Clubs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Club club = await db.Clubs.FindAsync(id);
            db.Clubs.Remove(club);
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
