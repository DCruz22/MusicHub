using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MusicHub.Models
{
    public class MusicHubDB : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
        public MusicHubDB() : base("AzureCon") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<MusicalStyle> MusicalStyles { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Project_Comment> Project_Comments { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<User_Instrument> User_Instruments { get; set; }
        public DbSet<User_MusicalStyle> User_MusicalStyles { get; set; }
        public DbSet<Project_Follower> Project_Followers { get; set; }
        public DbSet<Project_Instrument> Project_Instruments { get; set; }
        public DbSet<Project_Content> Project_Content { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<User_Badge> User_Badges { get; set; }
        public DbSet<Feed> Feeds { get; set; }
    }
}