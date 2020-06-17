using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using assignment_4.Models;

    public class AppliactionDbContext : DbContext
    {
        public AppliactionDbContext (DbContextOptions<AppliactionDbContext> options)
            : base(options)
        {
        }

        public DbSet<assignment_4.Models.Post> Post { get; set; }
    }
