using artPost.Models;
using artPost_.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace artPost_.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
            public DbSet<user> user {  get; set; }
            public DbSet<userList> userList { get; set; }

            public DbSet<Image> image { get; set; }

        
        }
    }
