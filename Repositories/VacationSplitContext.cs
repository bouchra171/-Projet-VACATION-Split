using System.Collections.Generic;
using System;
using VacationSplit.Models;
using Microsoft.EntityFrameworkCore;

namespace VacationSplit.Repositories
{
    public class VacationSplitContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LOCALHOST;Database=VacationSplit;TrustServerCertificate=true;Integrated Security=sspi");

        }
    }
}