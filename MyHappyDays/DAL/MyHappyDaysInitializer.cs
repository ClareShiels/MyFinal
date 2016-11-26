using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MyHappyDays.Models;


namespace MyHappyDays.DAL
{
    public class MyHappyDaysInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            //adding the entity details to the approprite DbSet property and saving the changes to the db
            //entering some child details for initialising the db
            var children = new List<Child>
            {
                new Child { ChildFirstName = "Cora", ChildLastName = "Shiels", GuardianEmail = "clareshiels@gmail.com",
                    FirstName = "Clare", LastName = "Shiels", GuardianPhNo = "0871234567",
                    DOB = DateTime.Parse("27-06-2009"), SpecialNeeds = false ,
                    AddressLine1 = "1 Prospect Meadows", AddressLine2 = "Stocking Lane", County = "Dublin 16", PermissionToLeave = false},
                new Child { ChildFirstName = "Noah", ChildLastName = "Shiels", GuardianEmail = "clareshiels@gmail.com",
                    FirstName = "Clare", LastName = "Shiels", GuardianPhNo = "0871234567",
                    DOB = DateTime.Parse("06-12-2007"), SpecialNeeds = false,
                    AddressLine1 = "1 Prospect Meadows", AddressLine2 = "Rathfarnham", County = "Dublin 16", EirCode = "D16H7RF",PermissionToLeave = false },
                new Child { ChildFirstName = "Rosie", ChildLastName = "Shiels", GuardianEmail = "dermotshiels@gmail.com",
                    FirstName = "Dermot", LastName = "Shiels", GuardianPhNo = "0871234567",
                    DOB = DateTime.Parse("17-07-2012"), SpecialNeeds = false ,
                    AddressLine1 = "1 Prospect Meadows", AddressLine2 = "Stocking Lane", County = "Dublin 16", PermissionToLeave = false},
                new Child { ChildFirstName = "Freya", ChildLastName = "Bucknell", GuardianEmail = "siobhan@gmail.com",
                    FirstName = "Siobhan", LastName = "Bucknell", GuardianPhNo = "0871231231",
                    DOB = DateTime.Parse("17-07-2007"), SpecialNeeds = false ,
                    AddressLine1 = "21 Marlfield Terrace", AddressLine2 = "Kiltipper", County = "Dublin 24", PermissionToLeave = false},
                new Child { ChildFirstName = "Mia", ChildLastName = "Bailey", GuardianEmail = "emma@gmail.com",
                    FirstName = "Emma", LastName = "Chandler", GuardianPhNo = "0864567879",
                    DOB = DateTime.Parse("31-12-2006"), SpecialNeeds = false ,
                    AddressLine1 = "349 Ryevale Lawns", AddressLine2 = "Leixlip", County = "Kildare", PermissionToLeave = false},
                new Child { ChildFirstName = "Sylvie", ChildLastName = "Bucknell", GuardianEmail = "siobhan@gmail.com",
                    FirstName = "Siobhan", LastName = "Bucknell", GuardianPhNo = "0871231231",
                    DOB = DateTime.Parse("18-01-2013"), SpecialNeeds = false ,
                    AddressLine1 = "21 Marlfield Terrace", AddressLine2 = "Kiltipper", County = "Dublin 24", PermissionToLeave = false},
                new Child { ChildFirstName = "Sam", ChildLastName = "Chandler", GuardianEmail = "chris@gmail.com",
                    FirstName = "Chris", LastName = "Chandler", GuardianPhNo = "0871231232",
                    DOB = DateTime.Parse("18-06-2013"), SpecialNeeds = false ,
                    AddressLine1 = "349 Ryevale Lawns", AddressLine2 = "Leixlip", County = "Kildare", PermissionToLeave = false},
                new Child { ChildFirstName = "Liam", ChildLastName = "Bucknell", GuardianEmail = "dale@gmail.com",
                    FirstName = "Dale", LastName = "Bucknell", GuardianPhNo = "0871231233",
                    DOB = DateTime.Parse("17-04-2015"), SpecialNeeds = false ,
                    AddressLine1 = "21 Marlfield Terrace", AddressLine2 = "Kiltipper", County = "Dublin 24", PermissionToLeave = false},

            };

            //seeding the Children DBSet
            //performing a SaveChanges method after each group of entities so as to help eliminate a possible problem
            children.ForEach(c => context.Children.Add(c));
            context.SaveChanges();

            var clubs = new List<Club>
            {
                new Club { ClubName = "St Marys BNS", ClubEmail = "activities@StMarys.com",  FirstName = "Paula",
                    LastName = "Byrne", ContactPhNo = "0871231234", AddressLine1 = "14 Grange Rd", AddressLine2 = "Rathfarnham", County = "Dublin 14", EirCode = "D14HR7"  },
                new Club { ClubName = "Loreto Primary School", ClubEmail = "activities@Loreto.com",  FirstName = "Jennifer",
                    LastName = "Dean", ContactPhNo = "0861231231", AddressLine1 = "Grange Rd", AddressLine2 = "Rathfarnham", County = "Dublin 14", EirCode = "D14HR8" },
                new Club { ClubName = "Templeogue Tennis Club", ClubEmail = "tennis@templeogueclub.com",  FirstName = "Jemma",
                    LastName = "Butler", ContactPhNo = "0861561231", AddressLine1 = "Templeogue Village", AddressLine2 = "Templeogue", County = "Dublin 6", EirCode = "D6HR8" },
                new Club { ClubName = "St Endas GAA", ClubEmail = "endasgaa@bllyboden.com",  FirstName = "Patrick",
                    LastName = "Sears", ContactPhNo = "0867891231", AddressLine1 = "Firhouse Rd", AddressLine2 = "Firhouse", County = "Dublin 24", EirCode = "D24HR8" }
            };

            //seeding the Clubs DBSet
            //performing a SaveChanges method after each group of entities so as to help eliminate a possible problem
            clubs.ForEach(a => context.Clubs.Add(a));
            context.SaveChanges();

            var instructors = new List<Instructor>
            {
                new Instructor { InstructorEmail = "coco@hotmail.com", InstructorFirstName = "Coco", InstructorLastName = "Belle", InstructorPhNo = "0872342343", },
                new Instructor { InstructorEmail = "dave@yahoo.com", InstructorFirstName = "David", InstructorLastName = "Gray", InstructorPhNo = "0866231231" },
                new Instructor { InstructorEmail = "john@gmail.com", InstructorFirstName = "John", InstructorLastName = "O'Shea", InstructorPhNo = "0871434241" },
                new Instructor { InstructorEmail = "daniel@hotmail.com", InstructorFirstName = "Daniel", InstructorLastName = "Bracken", InstructorPhNo = "0875871231" },
                new Instructor { InstructorEmail = "sandra@yahoo.com", InstructorFirstName = "Sandra", InstructorLastName = "Adamson", InstructorPhNo = "0871451287" },
            };

            instructors.ForEach(i => context.Instructors.Add(i));
            context.SaveChanges();


            var activities = new List<Activity>
            {
                new Activity { ActivityCourseStartDate = DateTime.Parse("12-06-2016"), ActivityCourseEndDate = DateTime.Parse("11-01-2017"),
                    ActivityType = ActivityType.Course, AgeGroup = AgeGroup.UnderSix, Day = DayOfWeek.Monday, NameOfActivity = "Basketball",
                    ClassTime = DateTime.Parse("14:30"), ClubID = 1, InstructorID = 1, PriceOfActivity = 80, MaxCapacity = 25,
                },
                new Activity { ActivityCourseStartDate = DateTime.Parse("01-06-2016"), ActivityCourseEndDate = DateTime.Parse("10-06-2017"),
                    ActivityType = ActivityType.Course, AgeGroup = AgeGroup.NineToTwelve, Day = DayOfWeek.Tuesday, NameOfActivity = "GAA Football",
                    ClassTime = DateTime.Parse("15:00"), ClubID = 2, InstructorID = 2, PriceOfActivity = 55, MaxCapacity = 30
                },
                new Activity { ActivityCourseStartDate = DateTime.Parse("30-06-2016"), ActivityCourseEndDate = DateTime.Parse("10-06-2017"),
                    ActivityType = ActivityType.Course, AgeGroup = AgeGroup.NineToTwelve, Day = DayOfWeek.Tuesday, NameOfActivity = "GAA Hurling",
                    ClassTime = DateTime.Parse("15:00"), ClubID = 2, InstructorID = 1, PriceOfActivity = 55, MaxCapacity = 29 }
            };

            activities.ForEach(a => context.Activities.Add(a));
            context.SaveChanges();

           

            var enrolments = new List<Enrolment>
            {
                new Enrolment { ChildID = 1, ActivityID = 1, PaymentDue = true, PaymentReceived = false },
                new Enrolment { ChildID = 1, ActivityID = 2, PaymentDue = false, PaymentReceived = true, },
                new Enrolment { ChildID = 2, ActivityID = 1, PaymentDue = true, PaymentReceived = false, }
            };

            enrolments.ForEach(e => context.Enrolments.Add(e));
            context.SaveChanges();

            

            var payments = new List<Payment>
            {
                new Payment {EnrolmentID = 1, DateReceived = DateTime.Parse("05-03-2016"), AmountDue = 80.00, AmountReceived = 80.00, PayeeName = "Clare Smith" },
                new Payment {EnrolmentID = 3, DateReceived = DateTime.Parse("02-02-2016"), AmountDue = 120.00, AmountReceived = 120.00, PayeeName = "Darren Byrne" },
                new Payment {EnrolmentID = 2, DateReceived = DateTime.Parse("12-06-2016"), AmountDue = 100.00, AmountReceived = 100.00, PayeeName = "Brenda Given" }
            };

            payments.ForEach(p => context.Payments.Add(p));
            context.SaveChanges();

        }
    }
}