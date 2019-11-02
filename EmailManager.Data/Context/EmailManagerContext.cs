using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.Context
{
    public class EmailManagerContext : DbContext
    {
        public EmailManagerContext( DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Client> Clients { get; set; }
        


    }
}
