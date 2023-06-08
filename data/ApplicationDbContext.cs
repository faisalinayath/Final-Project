

using System.ComponentModel;
using Microsoft.EntityFrameworkCore;                                    //this is an installed nuget package

using PropDealsNew.Models;

namespace PropDealsNew.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)

        {

        }
        public DbSet<Property> Property { get; set; }

        public DbSet<Register> Register { get; set; }

        public DbSet<Ad> Ads { get; set; }



    }
}