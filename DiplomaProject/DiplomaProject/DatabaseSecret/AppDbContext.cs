using Microsoft.EntityFrameworkCore;
using System;

namespace DiplomaProject.DatabaseSecret
{
    public class AppDbContext : ServiceDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
