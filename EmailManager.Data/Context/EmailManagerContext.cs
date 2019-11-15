using EmailManager.Data.Implementation;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmailManager.Data.Context
{
    public class EmailManagerContext : IdentityDbContext<User>
    {
        public EmailManagerContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<EmailBody> EmailBodies { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Loan-Client - Many-To-One
            modelBuilder.Entity<Loan>()
                .HasOne(m => m.Client)
                .WithMany(m => m.Loans)
                .HasForeignKey(m => m.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Loan-Email - One-To-One          
            //modelBuilder.Entity<Loan>()
            //    .HasKey(l => l.LoanId);

            //modelBuilder.Entity<Loan>()
            //    .HasOne(m => m.LoanEmail)
            //    .WithOne(m => m.Loan)
            //    .HasForeignKey<Email>(m => m.LoanId)
            //    .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Email-Loan - One-To-One 
            modelBuilder.Entity<Email>()
                .HasKey(l => l.Id );

            modelBuilder.Entity<Email>()
                .HasOne(m => m.Loan)
                .WithOne(m => m.LoanEmail)
                .HasForeignKey<Loan>(m => m.LoanEmailId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Email-Status - One-To-Many
            modelBuilder.Entity<Email>();

            modelBuilder.Entity<Email>()
                .HasOne(m => m.Status)
                .WithMany(m => m.Emails)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Email-User - Many-To-One
            //modelBuilder.Entity<Email>()
            //    .HasOne(m => m.User)
            //    .WithMany(m => m.UserEmails)
            //    .HasForeignKey(m => m.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region User-Email - One-To-Many
            //modelBuilder.Entity<User>()
            //    .HasMany(m => m.UserEmails)
            //    .WithOne(m => m.User)
            //    .HasForeignKey(m => m.EmailId)
            //    .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Email-EmailBody One-To-One
            modelBuilder.Entity<Email>()
                .HasOne(m=>m.EmailBody)
                .WithOne(m=>m.Email)
                .HasForeignKey<Email>(m=>m.EmailId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            base.OnModelCreating(modelBuilder);
        }

    }
}
