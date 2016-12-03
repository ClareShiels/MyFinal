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
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyHappyDays.ViewModels;

namespace MyHappyDays.Controllers
{
    public class ChildrenController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: All Children in the db
        // GET: Children sorted either by lastname or DOB
        [Authorize(Roles = "Club Manager, Admin")]
        //values for the sortOrder and searchString are passed into the Index action method from the query string in the URL
        public ActionResult Index(string sortOrder, string searchString)
        {
            //viewbag variables used to allow the view to configure the column heading hyperlinks with appropriate query string values
            //the query willbe either Name or DOB
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DOBSort = sortOrder == "DOB" ? "date_desc" : "DOB";
            var children = from c in db.Children
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

        //trying to get viewmodel to load data per user thurs night 1stdec 3:00am new low:( 
        [HttpGet]
        public ActionResult MyDashboard()//ApplicationUser user//)//, int? childID)
        {
            var myProfile = new ChildProfile();
            myProfile.Children = db.Children.
                Include(c => c.Enrolments);
                //Include(c => c.User);
            var currentID = User.Identity.GetUserId();
            //var childId = childID;
            myProfile.Children = db.Children.Where(c => c.UserID == currentID);
            //myProfile.Activities = myProfile.Enrolments
            return View(myProfile);
        }
        


        
        // GET: Children/Details/5
        public ActionResult  Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Child child = db.Children.Find(id);
            if (child == null)
            {
                return HttpNotFound();
            }

            return View(child);
        }

        // GET: Children/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Children/Create
        [HttpPost]
        //validateantiforgeryToken prevents cross-site request forgery attacks, needs corresponding html.antiforgeryToken()
        [ValidateAntiForgeryToken]      
        public async Task<ActionResult> Create([Bind(Include = "FirstName,LastName,GuardianPhNo,GuardianEmail,ChildLastName,ChildFirstName,AddressLine1,AddressLine2,County,EirCode,PermissionToLeave,DOB,SpecialNeeds")] Child child)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var userId = User.Identity.GetUserId();
                    child.UserID = userId;
                    db.Children.Add(child);
                    await db.SaveChangesAsync();
                    
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Can't save changes right now.  Try again, if the problem persists, check with your system administrator");
            }
            return View(child);

        }

        // GET: Children/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Child child = await db.Children.FindAsync(id);
            if (child == null)
            {
                return HttpNotFound();
            }
            return View(child);
        }

        // POST: Children/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,FirstName,LastName,GuardianPhNo,GuardianEmail,ChildLastName,ChildFirstName,AddressLine1,AddressLine2,County,EirCode,PermissionToLeave,DOB,SpecialNeeds")] Child child)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(child).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Can't save changes right now.  Try again, if the problem continues, check with your system administrator");
            }
            return View(child);
        }

        // GET: Children/Delete/5
        //manage error reporting with bool parameter
        public async Task<ActionResult> Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed at this time.  Try again, if problem continues, check with your system administrator";
            }
            Child child = await db.Children.FindAsync(id);
            if (child == null)
            {
                return HttpNotFound();
            }
            return View(child);
        }

        // POST: Children/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Child child = await db.Children.FindAsync(id);
                db.Children.Remove(child);
                await db.SaveChangesAsync();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
        //dispose method ensures the db connection is properly closed and the resources are freed up by getting rid of the context instance
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
