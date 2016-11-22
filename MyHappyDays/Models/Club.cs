using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyHappyDays.Models
{
    public class Club
    {
        //primary key - ClubID
        public int ID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First Name cannot be longer than 50 characters.")]
        [Column("FirstName")]
        [Display(Name = "Club Administrator's First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last Name cannot be longer than 50 characters.")]
        [Display(Name = "Club Administrator's Family Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + ", " + LastName;
            }
        }

        [Required(ErrorMessage = "Contact Number is Required")]
        [Display(Name = "Club Phone Number")]
        public string ContactPhNo { get; set; }

        [Required(ErrorMessage = "Email Address is Required")]
        [Display(Name = "Club Email Address")]
        [DataType(DataType.EmailAddress)]
        public string ClubEmail { get; set; }

        [Required(ErrorMessage = "Club Name is Required")]
        [StringLength(30, ErrorMessage = "Last Name cannot be longer than 30 characters.")]
        [Display(Name = "Club Name")]
        public string ClubName { get; set; }

        [Required(ErrorMessage = "Address Line 1 Required")]
        [Display(Name = "Address line 1")]
        public string AddressLine1 { get; set; }

        [Required(ErrorMessage = "Address Line 2 Required")]
        [Display(Name = "Address line 2")]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "County is Required")]
        [Display(Name = "County")]
        public string County { get; set; }

        [Display(Name = "Eircode")]
        public string EirCode { get; set; }

        //1:1 relationship between club and user
        public string UserID { get; set; }

        //Navigation Properties: 

        //implementing a 1:m relationship between activity centre and instructors
        public virtual ICollection<Instructor> Instructors { get; set; }
        //navigation property implementing a 1:m relationship between activity centre and activities
        public virtual ICollection<Activity> Activities { get; set; }

        //navigation property implementing a 1:1 relationship between Club Manager and Application User
        [ForeignKey("UserID")]
        public virtual ApplicationUser User { get; set; }
    }
}