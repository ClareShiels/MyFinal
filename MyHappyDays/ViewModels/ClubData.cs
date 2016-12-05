using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyHappyDays.Models;


namespace MyHappyDays.ViewModels
{
    public class ClubData
    {
        public IEnumerable<Club> Clubs { get; set; }
        public IEnumerable<Child> Children { get; set; }
        public IEnumerable<Enrolment> Enrolments { get; set; }
        public IEnumerable<Activity> Activities { get; set; }
       
        public IEnumerable<Instructor> Instructors { get; set; }
        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }
    }
}