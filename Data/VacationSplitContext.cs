using System.Collections.Generic;
using System;
using VacationSplit.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace VacationSplit.Data
{
    public class VacationSplitContext : DbContext
    {  
        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        public VacationSplitContext(DbContextOptions<VacationSplitContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Expense>().HasOne(g => g.Category).WithMany().OnDelete(DeleteBehavior.SetNull);
        }
    }
}