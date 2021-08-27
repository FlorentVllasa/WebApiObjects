using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiObjects.Models
{
    public class WebDbContext : DbContext
    {

        public WebDbContext(DbContextOptions<WebDbContext> options) : base(options)
        {
            
        }

        public DbSet<Model> Models { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Project> Projects { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Model>()
                .HasMany(m => m.SubModels)
                .WithOne();
                                
            modelBuilder.Entity<Property>()
                .HasOne(p => p.ParentModel)
                .WithMany();

            modelBuilder.Entity<Project>()
                .HasMany(p => p.Models)
                .WithOne();
                
                
        }
    }

}

