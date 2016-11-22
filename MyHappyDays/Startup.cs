//owin is open web interface for .Net, it defines standard interface between web server and .Net
//each Owin app has a startup class that is configured with different components
//the startup class contains the configuration method used to create roles and users
using Microsoft.Owin;
using Owin;
using MyHappyDays.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;


[assembly: OwinStartupAttribute(typeof(MyHappyDays.Startup))]
namespace MyHappyDays
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        // This method is used to create default User roles and Admin user for login of the app   
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // creating 1st admin role and default admin user
            if (!roleManager.RoleExists("Admin"))
            {
                // creating an Admin ROLE
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //creating default admin superUser to maintain the site, validation logic for usernames and pw is found in HappyDaysOne/App_Start/IndentityConfig.cs
                var user = new ApplicationUser();
                user.UserName = "ClareShiels";
                user.Email = "clare.cashin@gmail.com";
                //changed PW 10/10/16 to attempt overcome identity issues, ensuring its legit
                string userPWD = "Casho1!";

                //ok now - getting errors here now as of 25/9/16, no user created
                var chkUser = UserManager.Create(user, userPWD);

                //Add default superUser (Clare Shiels) to the Role of Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            // creating a clubManager role to be enabled to perform CRUD on activities and lecturers and R on children
            if (!roleManager.RoleExists("Club Manager"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Club Manager";
                roleManager.Create(role);

            }
            //reversed 15/10/16 and registering ok//commenting out below lines to troubleshoot through role assignment15/10/16
            // creating a Child's Guardian role to be enabled to perform CRUD on child and    
            if (!roleManager.RoleExists("Child's Guardian"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Child's Guardian";
                roleManager.Create(role);

            }
        }
    }
}



//using Microsoft.Owin;
//using Owin;

//[assembly: OwinStartupAttribute(typeof(MyHappyDays.Startup))]
//namespace MyHappyDays
//{
//    public partial class Startup
//    {
//        public void Configuration(IAppBuilder app)
//        {
//            ConfigureAuth(app);
//        }
//    }
//}
