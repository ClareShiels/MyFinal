using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using MyHappyDays.Models;
using Microsoft.AspNet.Identity.EntityFramework;


namespace MyHappyDays.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext(); 
        // GET: Users
        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var Currentuser = User.Identity;
            
                ViewBag.Name = Currentuser.Name;
                ViewBag.displayMenu = "No";

                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }

                else if (User.IsInRole("Club Manager"))
                {
                  
                    ViewBag.displayMenu = "Club Manager";
                    var userId = User.Identity.GetUserId();
                    ViewBag.UserID = userId;
                    ViewBag.ClubID = db.Clubs.Where(c => c.UserID == userId);
                }

                else if (User.IsInRole("Child's Guardian"))
                {
                    ViewBag.displayMenu = "Child's Guardian";
                    var userId = User.Identity.GetUserId();
                    ViewBag.UserID = User.Identity.GetUserId();
                    ViewBag.ChildID = db.Children.Where(k => k.UserID == userId);
                }


                return View();
            }
            else
            {
                ViewBag.Name = "Not Logged IN";
            }


            return View();


        }
    }
}