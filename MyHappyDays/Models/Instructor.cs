using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyHappyDays.Models
{
    public class Instructor
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Enter Instructor's First Name")]
        [StringLength(50, ErrorMessage = "Instructor's First Name cannot be longer than 50 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [Display(Name = "Instructor's First Name")]
        public string InstructorFirstName { get; set; }

        [Required(ErrorMessage = "Enter Instructor's Last Name")]
        [StringLength(50, ErrorMessage = "Instructor's Last Name cannot be longer than 50 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        [Display(Name = "Instructor's Last Name")]

        public string InstructorLastName { get; set; }

        [Display(Name = "Instructor's Full Name")]
        public string FullName
        {
            get
            {
                return InstructorFirstName + " " + InstructorLastName;
            }
        }
        public string InstructorEmail { get; set; }
        public string InstructorPhNo { get; set; }

        //navigation property implementing a m:m relationship between instructor and activity
        public virtual ICollection<Activity> Activities { get; set; }

    }
}