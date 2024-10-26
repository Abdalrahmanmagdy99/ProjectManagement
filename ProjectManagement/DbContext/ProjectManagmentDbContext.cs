using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Entities;
using System;

namespace ProjectManagement.DbContext
{
    public class ProjectManagmentDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ProjectManagmentDbContext(DbContextOptions<ProjectManagmentDbContext> options) : base(options)
        { }
        public override DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Entities.Task> Tasks { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);
        }
    }
}
