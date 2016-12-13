namespace MyHappyDays.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activity",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NameOfActivity = c.String(nullable: false),
                        MaxCapacity = c.Int(nullable: false),
                        AgeGroup = c.Int(nullable: false),
                        ActivityType = c.Int(nullable: false),
                        PriceOfActivity = c.Decimal(nullable: false, storeType: "money"),
                        ActivityCourseStartDate = c.DateTime(nullable: false),
                        ActivityCourseEndDate = c.DateTime(nullable: false),
                        Day = c.Int(nullable: false),
                        ClassTime = c.DateTime(nullable: false),
                        ClubID = c.Int(nullable: false),
                        InstructorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Club", t => t.ClubID, cascadeDelete: true)
                .ForeignKey("dbo.Instructor", t => t.InstructorID, cascadeDelete: true)
                .Index(t => t.ClubID)
                .Index(t => t.InstructorID);
            
            CreateTable(
                "dbo.Club",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        ContactPhNo = c.String(nullable: false),
                        ClubEmail = c.String(nullable: false),
                        ClubName = c.String(nullable: false, maxLength: 30),
                        AddressLine1 = c.String(nullable: false),
                        AddressLine2 = c.String(nullable: false),
                        County = c.String(nullable: false),
                        EirCode = c.String(),
                        UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Instructor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InstructorFirstName = c.String(nullable: false, maxLength: 50),
                        InstructorLastName = c.String(nullable: false, maxLength: 50),
                        InstructorEmail = c.String(),
                        InstructorPhNo = c.String(),
                        Club_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Club", t => t.Club_ID)
                .Index(t => t.Club_ID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Enrolment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PaymentMade = c.Boolean(nullable: false),
                        PaymentDue = c.Boolean(nullable: false),
                        ChildID = c.Int(nullable: false),
                        ActivityID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Activity", t => t.ActivityID, cascadeDelete: true)
                .ForeignKey("dbo.Child", t => t.ChildID, cascadeDelete: true)
                .Index(t => t.ChildID)
                .Index(t => t.ActivityID);
            
            CreateTable(
                "dbo.Child",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GuardianFirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        GuardianPhNo = c.String(nullable: false),
                        GuardianEmail = c.String(nullable: false),
                        ChildLastName = c.String(nullable: false, maxLength: 50),
                        ChildFirstName = c.String(nullable: false, maxLength: 50),
                        AddressLine1 = c.String(nullable: false),
                        AddressLine2 = c.String(nullable: false),
                        County = c.String(nullable: false),
                        EirCode = c.String(),
                        PermissionToLeave = c.Boolean(nullable: false),
                        DOB = c.DateTime(nullable: false),
                        UserID = c.String(maxLength: 128),
                        SpecialNeeds = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AmountPaid = c.Double(nullable: false),
                        AmountDue = c.Double(nullable: false),
                        DateReceived = c.DateTime(nullable: false),
                        PayeeName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Activity", "InstructorID", "dbo.Instructor");
            DropForeignKey("dbo.Enrolment", "ChildID", "dbo.Child");
            DropForeignKey("dbo.Child", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Enrolment", "ActivityID", "dbo.Activity");
            DropForeignKey("dbo.Activity", "ClubID", "dbo.Club");
            DropForeignKey("dbo.Club", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Instructor", "Club_ID", "dbo.Club");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Child", new[] { "UserID" });
            DropIndex("dbo.Enrolment", new[] { "ActivityID" });
            DropIndex("dbo.Enrolment", new[] { "ChildID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Instructor", new[] { "Club_ID" });
            DropIndex("dbo.Club", new[] { "UserID" });
            DropIndex("dbo.Activity", new[] { "InstructorID" });
            DropIndex("dbo.Activity", new[] { "ClubID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Payment");
            DropTable("dbo.Child");
            DropTable("dbo.Enrolment");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Instructor");
            DropTable("dbo.Club");
            DropTable("dbo.Activity");
        }
    }
}
