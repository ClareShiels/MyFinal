using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyHappyDays.Models;

namespace MyHappyDays.Controllers
{
    public class ChildrenController : Controller
    {
        private MyHappyDays.Models.ApplicationDbContext db = new MyHappyDays.Models.ApplicationDbContext();

        // GET: Children
        public ActionResult Index(string sortOrder, string searchString)
        {
            //viewbag variables used to allow the view to configure the column heading hyperlinks with appropriate query string values
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

            
        }
    }
