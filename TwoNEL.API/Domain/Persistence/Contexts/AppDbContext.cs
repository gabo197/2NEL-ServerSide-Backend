﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<FavoriteProfile> FavoriteProfiles { get; set; }
        public DbSet<FavoriteStartup> FavoriteStartups { get; set; }
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
                    },
                    new User
                    {
                        Id = 103,
                        Email = "a@gmail.com",
                        Password = "b",
                        RegisterDate = DateTime.Now
                    }
                );

            // FavoriteProfile Entity
            builder.Entity<FavoriteProfile>().ToTable("FavoriteProfiles");

            // Constraints
            builder.Entity<FavoriteProfile>().HasKey(fp => new { fp.UserId, fp.FavoriteId });

            // Relationships
            builder.Entity<FavoriteProfile>()
                .HasOne(ut => ut.Profile)
                .WithMany(u => u.FavoriteProfiles)
                .HasForeignKey(ut => ut.UserId);

            //builder.Entity<FavoriteProfile>()
            //    .HasOne(ut => ut.Favorite)
            //    .WithMany(u => u.FavoriteProfiles)
            //    .HasForeignKey(ut => ut.FavoriteId);

            // FavoriteStartup Entity
            builder.Entity<FavoriteStartup>().ToTable("FavoriteStartups");

            // Constraints
            builder.Entity<FavoriteStartup>().HasKey(fs => new { fs.UserId, fs.StartupId });

            // Relationships
            builder.Entity<FavoriteStartup>()
                .HasOne(ut => ut.Profile)
                .WithMany(u => u.FavoriteStartups)
                .HasForeignKey(ut => ut.UserId);

            builder.Entity<FavoriteStartup>()
                .HasOne(ut => ut.Startup)
                .WithMany(u => u.FavoriteStartups)
                .HasForeignKey(ut => ut.StartupId);

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
                        FirstName = "Peter",
                        LastName = "Castle",
                        ImageUrl = "https://elcomercio.pe/resizer/7tC5FuIbm2Vu2OkRBGOPDghCET8=/580x330/smart/filters:format(jpeg):quality(75)/cloudfront-us-east-1.images.arcpublishing.com/elcomercio/GI3VVEFCY5BL5LEVPG6F6SIC5Y.jpg",
                        Occupation = "Leftist activist",
                        City = "Chota",
                        ProfileTags = null,
                        Requests = null,
                        Description = "Profesor con el sueño de crear una revolución y un verdadero cambio en el país"
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
                        ImageUrl = "https://i.pinimg.com/originals/3b/8a/d2/3b8ad2c7b1be2caf24321c852103598a.jpg",
                        City = "Lima",
                        ProfileTags = null,
                        Requests = null,
                        Specialty = "C++",
                        Description = "Programadora egresada de Cibertec con las habilidades de C++, C#, Python, Java, Javascript, Typescript y nociones de aplicaciones web"
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
                        ImageUrl = "https://www.hp.com/us-en/shop/app/assets/images/uploads/prod/25-best-hd-wallpapers-laptops159561982840438.jpg",
                        City = "Arequipa",
                        ProfileTags = null,
                        Requests = null,
                        Description = "Economista egresado de la Universidad del Pacífico que busca apoyar económicamente a alguna startup a la cual le vea futuro"
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
                        ImageUrl = "https://humanwindow.com/wp-content/uploads/cristiano-ronaldo-vegan.jpg",
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
                        ImageUrl = "https://i.ytimg.com/vi/YDFt_-DH6cg/maxresdefault.jpg",
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

