using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHappyDays.Models
{
    public class Instructor
    {
        public int ID { get; set; }
        public string InstructorFirstName { get; set; }
        public string InstructorLastName { get; set; }
        [Display(Name = "Instructor's Full Name")]
        public string FullName
        {
            get
            {
                return InstructorLastName + ", " + InstructorFirstName;
            }
        }
        public string InstructorEmail { get; set; }
        public string InstructorPhNo { get; set; }

        //navigation property implementing a m:m relationship between instructor and activity
        public virtual ICollection<Activity> Activities { get; set; }

    }
}