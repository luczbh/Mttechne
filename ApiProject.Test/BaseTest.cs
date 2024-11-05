using Microsoft.Extensions.DependencyInjection;
using Moq;
using Serilog;

namespace ApiProject.Test
{
    public abstract class BaseTest
    {
        protected Mock<ILogger> Log;

        public BaseTest()
        {
            //Log = new LoggerConfiguration().WriteTo.InMemory().CreateLogger();

            var services = new ServiceCollection();
            var serviceProvider = services.BuildServiceProvider();

            Log = new Mock<ILogger>();
            Log.Setup(_ => _.Debug(It.IsAny<string>(), It.IsAny<object>(), null));
            Log.Setup(_ => _.Warning(It.IsAny<string>(), It.IsAny<object>(), null));
            Log.Setup(_ => _.Information(It.IsAny<string>(), It.IsAny<object>(), null));
            Log.Setup(_ => _.Error(It.IsAny<string>(), It.IsAny<object>(), null));
        }


        //protected ApplicationUser LoggedUserAdmin
        //{
        //    get
        //    {
        //        var appUserRoles = new List<ApplicationUserRole> { new ApplicationUserRole { RoleId = "5", Role = new ApplicationRole { Name = "Administrator" } } };

        //        return new ApplicationUser
        //        {
        //            Gid = "Z1",
        //            Roles = appUserRoles
        //        };
        //    }
        //}

        //protected ApplicationUser LoggedUserBasic
        //{
        //    get
        //    {
        //        var appUserRoles = new List<ApplicationUserRole> { new ApplicationUserRole { RoleId = "1", Role = new ApplicationRole { Name = "Basic" } } };

        //        return new ApplicationUser
        //        {
        //            Gid = "Z1",
        //            Roles = appUserRoles
        //        };
        //    }
        //}

    }
}
