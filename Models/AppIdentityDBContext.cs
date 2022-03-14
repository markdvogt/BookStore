using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class AppIdentityDBContext : IdentityDbContext<IdentityUser> //This comes from the security NuGet package we installed
    {
        public AppIdentityDBContext(DbContextOptions options) : base(options)
        {
        }
    }
}
