using System.Collections.Generic;
using System;
using VacationSplit.Models;
using Microsoft.EntityFrameworkCore;

namespace VacationSplit.Data
{
    public class VacationSplitContext : DbContext
    {    
        public VacationSplitContext(DbContextOptions<VacationSplitContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=LOCALHOST;Database=VacationSplit;TrustServerCertificate=true;Integrated Security=sspi");

        //}
    }
}