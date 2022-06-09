using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreClean.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreClean.Infra.Data.Context
{
    public class ProjectDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ProjectDbContext() : base()
        {

        }
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Follow> Follows { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Follow>()                                            //  1.
                .HasKey(k => new { k.FollowerId, k.FolloweeId });

            builder.Entity<Follow>()                                            //  2.
                .HasOne(u => u.Followee)
                .WithMany(u => u.Follower)
                .HasForeignKey(u => u.FolloweeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Follow>()                                            //  3.
                .HasOne(u => u.Follower)
                .WithMany(u => u.Followee)
                .HasForeignKey(u => u.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Photo>()
                .HasMany(e => e.Comments)
                .WithOne(e => e.Photo)
                .OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<Category>()
            //    .Property(f => f.Id)
            //    .ValueGeneratedOnAdd();
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>()
        //        .HasMany(x => x.Followers).WithMany(x => x.Following)
        //        .Map(x => x.ToTable("Followers")
        //            .MapLeftKey("UserId")
        //            .MapRightKey("FollowerId"));
        //}
    }

    
}
