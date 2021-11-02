using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        public DbSet<Action> Actions { get; set; }
        public DbSet<ModelType> ModelTypes { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ModelType>()
                .HasMany(mt => mt.Models)
                .WithOne();

            modelBuilder.Entity<ModelType>()
                .HasMany(mt => mt.DataVariables)
                .WithOne();

            modelBuilder.Entity<Type>()
                .HasOne(t => t.ParentModelType)
                .WithMany()
                .HasForeignKey(t => t.ParentModelTypeId);

            modelBuilder.Entity<Model>()
                .HasMany(m => m.children)
                .WithOne(m => m.ParentModel)
                .HasForeignKey(m => m.ParentId);

            modelBuilder.Entity<Property>()
                .HasOne(p => p.ParentModel)
                .WithMany()
                .HasForeignKey(p => p.ParentId);

            modelBuilder.Entity<Project>();

            modelBuilder.Entity<Action>()
                .HasOne(a => a.ParentModel)
                .WithMany()
                .HasForeignKey(a => a.ParentId);

            modelBuilder.Entity<Tag>()
                .HasOne(t => t.ParentModel)
                .WithMany()
                .HasForeignKey(t => t.ParentModelID);

            modelBuilder.Entity<Tag>()
                .Property(t => t.Tags)
                .HasConversion(t => JsonConvert.SerializeObject(t), t => JsonConvert.DeserializeObject<List<string>>(t));

        }
    }

}

