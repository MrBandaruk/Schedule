using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Schedule.DAL.Entities;

namespace Schedule.DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
        {
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = DateTime.Now.AddYears(100).TimeOfDay;          
        }
    }
}
