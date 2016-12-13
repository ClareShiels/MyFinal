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
                new Child { ChildFirstName = "Cora", ChildLastName = "Shiels", GuardianEmail = "clarecashin@gmail.com",
                    FirstName = "Clare", LastName = "Shiels", GuardianPhNo = "0871234567",
                    DOB = DateTime.Parse("27-06-2009"), SpecialNeeds = false ,
                    AddressLine1 = "1 Prospect Meadows", AddressLine2 = "Stocking Lane", County = "Dublin 16", PermissionToLeave = false},

                new Child { ChildFirstName = "Noah", ChildLastName = "Shiels", GuardianEmail = "clarecashin@gmail.com",
                    FirstName = "Clare", LastName = "Shiels", GuardianPhNo = "0871234567",
                    DOB = DateTime.Parse("06-12-2007"), SpecialNeeds = false,
                    AddressLine1 = "1 Prospect Meadows", AddressLine2 = "Rathfarnham", County = "Dublin 16", EirCode = "D16H7RF",PermissionToLeave = false },

                new Child { ChildFirstName = "Rosie", ChildLastName = "Shiels", GuardianEmail = "clarecashin@gmail.com",
                    FirstName = "Clare", LastName = "Shiels", GuardianPhNo = "0871234567",
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

                new Child { ChildFirstName = "Sam", ChildLastName = "Chandler", GuardianEmail = "emma@gmail.com",
                    FirstName = "Emma", LastName = "Chandler", GuardianPhNo = "0864567879",
                    DOB = DateTime.Parse("18-06-2013"), SpecialNeeds = false ,
                    AddressLine1 = "349 Ryevale Lawns", AddressLine2 = "Leixlip", County = "Kildare", PermissionToLeave = false},

                new Child { ChildFirstName = "Liam", ChildLastName = "Bucknell", GuardianEmail = "siobhan@gmail.com",
                    FirstName = "Siobhan", LastName = "Bucknell", GuardianPhNo = "0871231231",
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
                    LastName = "Sears", ContactPhNo = "0867891231", AddressLine1 = "Firhouse Rd", AddressLine2 = "Firhouse", County = "Dublin 24", EirCode = "D24HR8" },

                new Club { ClubName = "Ballyroan Community Centre", ClubEmail = "communitycentre@ballyroan.com",  FirstName = "Saoirse",
                    LastName = "Mehigan", ContactPhNo = "0867611231", AddressLine1 = "12 Ballyroan Rd", AddressLine2 = "Ballyroan", County = "Dublin 12", EirCode = "D12HR9"},

                new Club { ClubName = "Tymon Bawn Community Centre", ClubEmail = "tymonbawn@community.com",  FirstName = "Gemma",
                    LastName = "O'Sullivan", ContactPhNo = "0865691231", AddressLine1 = "Firhouse Rd West", AddressLine2 = "Old Bawn", County = "Dublin 24", EirCode = "D24HR1" },
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

                new Instructor { InstructorEmail = "betty@yahoo.com", InstructorFirstName = "Betty", InstructorLastName = "Byrne", InstructorPhNo = "0871451997" },
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
                    ClassTime = DateTime.Parse("15:00"), ClubID = 2, InstructorID = 1, PriceOfActivity = 55, MaxCapacity = 29
                },

                new Activity { ActivityCourseStartDate = DateTime.Parse("30-02-2017"), ActivityCourseEndDate = DateTime.Parse("10-12-2017"),
                    ActivityType = ActivityType.Course, AgeGroup = AgeGroup.SixToNine, Day = DayOfWeek.Tuesday, NameOfActivity = "GAA Football",
                    ClassTime = DateTime.Parse("15:00"), ClubID = 2, InstructorID = 2, PriceOfActivity = 55, MaxCapacity = 30
                },
                
                new Activity { ActivityCourseStartDate = DateTime.Parse("30-02-2017"), ActivityCourseEndDate = DateTime.Parse("10-12-2017"),
                    ActivityType = ActivityType.Course, AgeGroup = AgeGroup.SixToNine, Day = DayOfWeek.Tuesday, NameOfActivity = "GAA Hurling",
                    ClassTime = DateTime.Parse("15:00"), ClubID = 2, InstructorID = 1, PriceOfActivity = 55, MaxCapacity = 30
                },

                 new Activity { ActivityCourseStartDate = DateTime.Parse("30-02-2017"), ActivityCourseEndDate = DateTime.Parse("10-12-2017"),
                    ActivityType = ActivityType.Course, AgeGroup = AgeGroup.UnderSix, Day = DayOfWeek.Tuesday, NameOfActivity = "GAA Academy",
                    ClassTime = DateTime.Parse("14:45"), ClubID = 2, InstructorID = 2, PriceOfActivity = 55, MaxCapacity = 20
                 },

                  new Activity { ActivityCourseStartDate = DateTime.Parse("31-01-2017"), ActivityCourseEndDate = DateTime.Parse("10-12-2017"),
                    ActivityType = ActivityType.Course, AgeGroup = AgeGroup.SixToNine, Day = DayOfWeek.Wednesday, NameOfActivity = "ArtsNCrafts",
                    ClassTime = DateTime.Parse("15:00"), ClubID = 1, InstructorID = 3, PriceOfActivity = 55, MaxCapacity = 25
                 },

                   new Activity { ActivityCourseStartDate = DateTime.Parse("30-06-2016"), ActivityCourseEndDate = DateTime.Parse("10-06-2017"),
                    ActivityType = ActivityType.Course, AgeGroup = AgeGroup.NineToTwelve, Day = DayOfWeek.Tuesday, NameOfActivity = "GAA Hurling",
                    ClassTime = DateTime.Parse("15:00"), ClubID = 2, InstructorID = 1, PriceOfActivity = 55, MaxCapacity = 29
                 },

                   new Activity { ActivityCourseStartDate = DateTime.Parse("30-06-2016"), ActivityCourseEndDate = DateTime.Parse("10-06-2017"),
                    ActivityType = ActivityType.Course, AgeGroup = AgeGroup.NineToTwelve, Day = DayOfWeek.Tuesday, NameOfActivity = "GAA Hurling",
                    ClassTime = DateTime.Parse("15:00"), ClubID = 2, InstructorID = 1, PriceOfActivity = 55, MaxCapacity = 29
                  },

                    new Activity { ActivityCourseStartDate = DateTime.Parse("30-06-2016"), ActivityCourseEndDate = DateTime.Parse("10-06-2017"),
                    ActivityType = ActivityType.Course, AgeGroup = AgeGroup.NineToTwelve, Day = DayOfWeek.Tuesday, NameOfActivity = "GAA Hurling",
                    ClassTime = DateTime.Parse("15:00"), ClubID = 2, InstructorID = 1, PriceOfActivity = 55, MaxCapacity = 29
                   },

                    new Activity { ActivityCourseStartDate = DateTime.Parse("30-06-2016"), ActivityCourseEndDate = DateTime.Parse("10-06-2017"),
                    ActivityType = ActivityType.Course, AgeGroup = AgeGroup.NineToTwelve, Day = DayOfWeek.Tuesday, NameOfActivity = "GAA Hurling",
                    ClassTime = DateTime.Parse("15:00"), ClubID = 2, InstructorID = 1, PriceOfActivity = 55, MaxCapacity = 29
                   },

                    new Activity { ActivityCourseStartDate = DateTime.Parse("30-06-2016"), ActivityCourseEndDate = DateTime.Parse("10-06-2017"),
                    ActivityType = ActivityType.Course, AgeGroup = AgeGroup.NineToTwelve, Day = DayOfWeek.Tuesday, NameOfActivity = "GAA Hurling",
                    ClassTime = DateTime.Parse("15:00"), ClubID = 2, InstructorID = 1, PriceOfActivity = 55, MaxCapacity = 29
                   }
                };

            activities.ForEach(a => context.Activities.Add(a));
            context.SaveChanges();

           

            var enrolments = new List<Enrolment>
            {
                new Enrolment { ChildID = 1, ActivityID = 3, PaymentDue = false},

                new Enrolment { ChildID = 2, ActivityID = 2, PaymentDue = false},

                new Enrolment { ChildID = 3, ActivityID = 5, PaymentDue = false},

                new Enrolment { ChildID = 4, ActivityID = 4, PaymentDue = true},

                new Enrolment { ChildID = 5, ActivityID = 8, PaymentDue = true},

                new Enrolment { ChildID = 6, ActivityID = 11, PaymentDue = false},

                new Enrolment { ChildID = 7, ActivityID = 5, PaymentDue = true},

                new Enrolment { ChildID = 8, ActivityID = 6, PaymentDue = false},

                new Enrolment { ChildID = 7, ActivityID = 9, PaymentDue = true},

                new Enrolment { ChildID = 3, ActivityID = 10, PaymentDue = false},

                new Enrolment { ChildID = 2, ActivityID = 1, PaymentDue = true}
            };

            enrolments.ForEach(e => context.Enrolments.Add(e));
            context.SaveChanges();

            

            var payments = new List<Payment>
            {
                new Payment { DateReceived = DateTime.Parse("05-03-2016"), AmountDue = 80.00, AmountPaid = 80.00, PayeeName = "Clare Cashin" },

                new Payment { DateReceived = DateTime.Parse("02-02-2016"), AmountDue = 120.00, AmountPaid = 120.00, PayeeName = "Siobhan Bucknell" },

                new Payment { DateReceived = DateTime.Parse("12-06-2016"), AmountDue = 100.00, AmountPaid = 100.00, PayeeName = "Brenda Given" },

                new Payment { DateReceived = DateTime.Parse("12-06-2016"), AmountDue = 100.00, AmountPaid = 100.00, PayeeName = "Emma Chandler" },

                new Payment { DateReceived = DateTime.Parse("12-06-2016"), AmountDue = 100.00, AmountPaid = 100.00, PayeeName = "Brenda Given" },

                new Payment { DateReceived = DateTime.Parse("12-06-2016"), AmountDue = 100.00, AmountPaid = 100.00, PayeeName = "Emma Chandler" },

                new Payment { DateReceived = DateTime.Parse("12-06-2016"), AmountDue = 100.00, AmountPaid = 100.00, PayeeName = "Brenda Given" },

                new Payment { DateReceived = DateTime.Parse("12-06-2016"), AmountDue = 100.00, AmountPaid = 100.00, PayeeName = "Dermot Shiels" },

                new Payment { DateReceived = DateTime.Parse("12-06-2016"), AmountDue = 100.00, AmountPaid = 100.00, PayeeName = "Dale Bucknell" },

                new Payment { DateReceived = DateTime.Parse("12-06-2016"), AmountDue = 100.00, AmountPaid = 100.00, PayeeName = "Brenda Given" },

                new Payment { DateReceived = DateTime.Parse("12-06-2016"), AmountDue = 100.00, AmountPaid = 100.00, PayeeName = "Chris Chandler" },

            };

            payments.ForEach(p => context.Payments.Add(p));
            context.SaveChanges();

        }
    }
}