﻿// <auto-generated />
using System;
using EmailManager.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmailManager.Data.Migrations
{
    [DbContext(typeof(EmailManagerContext))]
    partial class EmailManagerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EmailManager.Data.Client", b =>
                {
                    b.Property<int>("IdClient")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EGNClient");

                    b.Property<string>("EmailClient");

                    b.Property<string>("FirstNameClient");

                    b.Property<string>("LastNameClient");

                    b.Property<string>("PhoneNumberClient");

                    b.HasKey("IdClient");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("EmailManager.Data.Email", b =>
                {
                    b.Property<int>("EmailId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AttachmentCount");

                    b.Property<double>("AttachmentSize");

                    b.Property<string>("Body");

                    b.Property<DateTime>("CurrentStatus");

                    b.Property<int?>("EmailStatusStatusID");

                    b.Property<DateTime>("FirstRegistration");

                    b.Property<bool>("IsValid");

                    b.Property<int>("LoanEmailID");

                    b.Property<DateTime>("ReceiveDate");

                    b.Property<string>("Sender");

                    b.Property<string>("Subject");

                    b.Property<DateTime>("TerminalStatus");

                    b.Property<string>("UserId");

                    b.HasKey("EmailId");

                    b.HasIndex("EmailStatusStatusID");

                    b.HasIndex("UserId");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("EmailManager.Data.Loan", b =>
                {
                    b.Property<int>("LoanId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClientIdClient");

                    b.Property<DateTime>("DateAsigned");

                    b.Property<int>("LoanEmailID");

                    b.Property<decimal>("LoanedSum");

                    b.HasKey("LoanId");

                    b.HasIndex("ClientIdClient");

                    b.HasIndex("LoanEmailID")
                        .IsUnique();

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("EmailManager.Data.Status", b =>
                {
                    b.Property<int>("StatusID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActionTaken");

                    b.Property<string>("Closed");

                    b.Property<string>("InitialApplication");

                    b.Property<DateTime>("LastStatus");

                    b.Property<string>("New");

                    b.Property<DateTime>("NewStatus");

                    b.Property<string>("NotReviewed");

                    b.Property<string>("Open");

                    b.Property<DateTime>("TimeStamp");

                    b.HasKey("StatusID");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("EmailManager.Data.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Role");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EmailManager.Data.Email", b =>
                {
                    b.HasOne("EmailManager.Data.Status", "EmailStatus")
                        .WithMany("Emails")
                        .HasForeignKey("EmailStatusStatusID");

                    b.HasOne("EmailManager.Data.User", "User")
                        .WithMany("UserEmails")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("EmailManager.Data.Loan", b =>
                {
                    b.HasOne("EmailManager.Data.Client", "Client")
                        .WithMany("Loans")
                        .HasForeignKey("ClientIdClient");

                    b.HasOne("EmailManager.Data.Email", "LoanEmail")
                        .WithOne("Loan")
                        .HasForeignKey("EmailManager.Data.Loan", "LoanEmailID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
