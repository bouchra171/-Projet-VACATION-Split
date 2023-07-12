using System.Collections.Generic;
using System;
using VacationSplit.Models;
using Microsoft.EntityFrameworkCore;

namespace VacationSplit.Repositories
{
    public class VacationSplitContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        {

            // I add a connection to a database instance while the context configures

            optionsBuilder.UseSqlServer(

                @"Server=LOCALHOST;Database=VacationSplit;TrustServerCertificate=true;Integrated Security=sspi");

        }
    }
}
