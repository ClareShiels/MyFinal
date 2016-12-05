using System;
using System.Collections.Generic;
using MyHappyDays.Models;


namespace MyHappyDays.ViewModels
{
    public class ChildProfile
    {
        public IEnumerable<ApplicationIdentity> Users { get; set; }
        public IEnumerable<Child> Children { get; set; }
        public IEnumerable<Enrolment> Enrolments { get; set; }
        public IEnumerable<Activity> Activities { get; set; }

    }
}
