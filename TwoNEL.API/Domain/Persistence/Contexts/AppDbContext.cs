using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain;
using TwoNEL.API.Domain.Models;
using TwoNEL.API.Extensions;

namespace TwoNEL.API.Domain.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Entrepreneur> Entrepreneurs { get; set; }
        public DbSet<Freelancer> Freelancers { get; set; }
        public DbSet<Investor> Investors { get; set; }
        public DbSet<ProfileTag> ProfileTags { get; set; }
        //public DbSet<ProfileRequest> ProfileRequests { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<Models.Startup> Startups { get; set; }
        public DbSet<StartupTag> StartupTags { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // User Entity
            builder.Entity<User>().ToTable("Users");

            //Constraints
            builder.Entity<User>().HasKey(u => u.Id);
            builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(u => u.Email).IsRequired();
            builder.Entity<User>().Property(u => u.Password).IsRequired();
            builder.Entity<User>().Property(u => u.RegisterDate).IsRequired();

            //Relationships
            builder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<Profile>(p => p.UserId);

            builder.Entity<User>()
                .HasOne(u => u.CreditCard)
                .WithOne(c => c.User)
                .HasForeignKey<CreditCard>(c => c.UserId);

            //Seed Data
            builder.Entity<User>().HasData
                (
                    new User
                    {
                        Id = 100,
                        Email = "marksloan12@gmail.com",
                        Password = "marksitouwu123",
                        RegisterDate = DateTime.Now

                    },
                    new User
                    {
                        Id = 101,
                        Email = "SaraQueen@gmail.com",
                        Password = "Sara123Queen123",
                        RegisterDate = DateTime.Now
                    },
                    new User
                    {
                        Id = 102,
                        Email = "derekshepherd@gmail.com",
                        Password = "12345",
                        RegisterDate = DateTime.Now
                    }
                ) ; ; 

            // Profile Entity
            builder.Entity<Profile>().ToTable("Profiles");

            //Constraints
            builder.Entity<Profile>().HasKey(p => p.UserId);
            builder.Entity<Profile>().HasDiscriminator<int>("user_type")
                .HasValue<Entrepreneur>(1)
                .HasValue<Freelancer>(2)
                .HasValue<Investor>(3);
            builder.Entity<Profile>().Property(p => p.UserId).IsRequired();
            builder.Entity<Profile>().Property(p => p.MembershipType).IsRequired();
            builder.Entity<Profile>().Property(p => p.FirstName).IsRequired();
            builder.Entity<Profile>().Property(p => p.LastName).IsRequired();

            //Seed Data
            builder.Entity<Entrepreneur>().HasData
                (
                    new Entrepreneur
                    {
                        UserId = 100,
                        MembershipType = EMembershipType.Free,
                        FirstName = "Mark",
                        LastName = "Sloan",
                        Portfolio = "",
                        ProfileTags = null,
                        Requests = null
                    }
                );

            builder.Entity<Freelancer>().HasData
                (
                    new Freelancer
                    {
                        UserId = 101,
                        MembershipType = EMembershipType.Free,
                        FirstName = "Sara",
                        LastName = "Queen",
                        Portfolio = "",
                        ProfileTags = null,
                        Requests = null,
                        Specialty = "C++"
                    }
                );

            builder.Entity<Investor>().HasData
                (
                    new Investor
                    {
                        UserId = 102,
                        MembershipType = EMembershipType.Free,
                        FirstName = "Derek",
                        LastName = "Shepherd",
                        Portfolio = "",
                        ProfileTags = null,
                        Requests = null
                    }
                );

            // Relationships

            builder.Entity<Entrepreneur>()
                .HasOne(e => e.Enterprise)
                .WithOne(e => e.Entrepreneur)
                .HasForeignKey<Enterprise>(e => e.EntrepreneurId);

            // ProfileTag Entity
            builder.Entity<ProfileTag>().ToTable("ProfileTags");

            //Constraints
            builder.Entity<ProfileTag>().HasKey(ut => new { ut.UserId, ut.TagId });

            // Relationships
            builder.Entity<ProfileTag>()
                .HasOne(ut => ut.Profile)
                .WithMany(u => u.ProfileTags)
                .HasForeignKey(ut => ut.UserId);

            builder.Entity<ProfileTag>()
                .HasOne(ut => ut.Tag)
                .WithMany(u => u.ProfileTags)
                .HasForeignKey(ut => ut.TagId);

            // Tag Entity
            builder.Entity<Tag>().ToTable("Tags");

            // Constraints
            builder.Entity<Tag>().HasKey(t => t.Id);
            builder.Entity<Tag>().Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Tag>().Property(t => t.Name).IsRequired().HasMaxLength(30);

            // Seed data

            // Enterprise Entity
            builder.Entity<Enterprise>().ToTable("Enterprises");

            // Constraints
            builder.Entity<Enterprise>().HasKey(e => e.EntrepreneurId);
            //builder.Entity<Enterprise>().Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Enterprise>().Property(e => e.Name).IsRequired();
            builder.Entity<Enterprise>().Property(e => e.Description).IsRequired();
            builder.Entity<Enterprise>().Property(e => e.BusinessEmail).IsRequired();
            builder.Entity<Enterprise>().Property(e => e.CorpNumber).IsRequired();
            builder.Entity<Enterprise>().Property(e => e.RegisterDate).IsRequired();

            // Relationships
            //builder.Entity<Enterprise>()
            //    .HasOne(e => e.Entrepreneur)
            //    .WithOne(e => e.Enterprise)
            //    .HasForeignKey<Enterprise>(e => e.EntrepreneurId);

            builder.Entity<Enterprise>()
                .HasMany(p => p.StartUps)
                .WithOne(p => p.Enterprise)
                .HasForeignKey(p => p.EnterpriseId);
            
            // Seed data
            builder.Entity<Enterprise>().HasData
                (
                    new Enterprise 
                    {
                        EntrepreneurId = 100, 
                        Name = "CR7 Fans",
                        Description = "Artículos de CR7",
                        BusinessEmail = "cr7siu@gmail.com",
                        CorpNumber = "969779077",
                        RegisterDate = DateTime.Now
                    }
                );


            // Startup Entity
            builder.Entity<Models.Startup>().ToTable("Startups");

            //Constraints
            builder.Entity<Models.Startup>().HasKey(s => s.Id);
            builder.Entity<Models.Startup>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Models.Startup>().Property(s => s.Name).IsRequired();
            builder.Entity<Models.Startup>().Property(s => s.Name).IsRequired();

            //Seed Data
            builder.Entity<Models.Startup>().HasData
                (
                    new Models.Startup
                    {
                        Id = 100,
                        EnterpriseId = 100,
                        Name = "Messi",
                        Description = "messi",
                        StartupTags = null
                    }
                ) ;

            // StartupTag Entity
            builder.Entity<StartupTag>().ToTable("StartupTags");

            //Constraints
            builder.Entity<StartupTag>().HasKey(st => new { st.StartupId, st.TagId });

            // Relationships
            builder.Entity<StartupTag>()
                .HasOne(st => st.Startup)
                .WithMany(s => s.StartupTags)
                .HasForeignKey(st => st.StartupId);

            builder.Entity<StartupTag>()
                .HasOne(st => st.Tag)
                .WithMany(s => s.StartupTags)
                .HasForeignKey(st => st.TagId);

            // CreditCard Entity
            builder.Entity<CreditCard>().ToTable("CreditCards");

            //Constraints
            builder.Entity<CreditCard>().HasKey(c => c.UserId);
            builder.Entity<CreditCard>().Property(c => c.CardNumber).IsRequired();
            builder.Entity<CreditCard>().Property(c => c.Cvv).IsRequired();
            builder.Entity<CreditCard>().Property(c => c.ExpMonth).IsRequired();
            builder.Entity<CreditCard>().Property(c => c.ExpYear).IsRequired();

            //Seed Data
            builder.Entity<CreditCard>().HasData
                (
                    new CreditCard
                    {
                        UserId = 100,
                        CardNumber = "4951235174810216",
                        Cvv = "791",
                        ExpMonth = "07",
                        ExpYear = "24"
                    },
                    new CreditCard
                    {
                        UserId = 101,
                        CardNumber = "7654247912374024",
                        Cvv = "234",
                        ExpMonth = "05",
                        ExpYear = "23",
                    },
                    new CreditCard
                    {
                        UserId = 102,
                        CardNumber = "1234567890123456",
                        Cvv = "123",
                        ExpMonth = "02",
                        ExpYear = "25",
                    }
                );

            // Request Entity
            builder.Entity<Request>().ToTable("Requests");

            //Constraints 
            builder.Entity<Request>().HasKey(c => c.Id);
            builder.Entity<Request>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Request>().Property(c => c.Subject).IsRequired();

            //// ProfileRequest Entity
            //builder.Entity<ProfileRequest>().ToTable("ProfileRequests");

            ////Constraints
            //builder.Entity<ProfileRequest>().HasKey(pr => new { pr.UserId, pr.RequestId });

            //// Relationships
            //builder.Entity<ProfileRequest>()
            //    .HasOne(pr => pr.Profile)
            //    .WithMany(p => p.ProfileRequests)
            //    .HasForeignKey(pr => pr.UserId);

            //builder.Entity<ProfileRequest>()
            //    .HasOne(pr => pr.Request)
            //    .WithMany(p => p.ProfileRequests)
            //    .HasForeignKey(pr => pr.RequestId);

            builder.Entity<Request>()
                .HasOne(r => r.Sender)
                .WithMany(f => f.Requests)
                .HasForeignKey(r => r.SenderId);

            builder.ApplySnakeCaseNamingConvention();
        }
    }
}

