using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<AppUser> Users { get; set; }

        //public DbSet<Photo> Photos { get; set; }  X wrong
        // we can create a Dbset of Photos but we need phots inside specific User photo Collection not 
        // like it will be Independently called like a User from DataBase
    }
}