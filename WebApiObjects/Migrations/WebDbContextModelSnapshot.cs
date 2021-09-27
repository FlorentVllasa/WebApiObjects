﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiObjects.Models;

namespace WebApiObjects.Migrations
{
    [DbContext(typeof(WebDbContext))]
    partial class WebDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApiObjects.Models.Action", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Method")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ParentId");

                    b.ToTable("Actions");
                });

            modelBuilder.Entity("WebApiObjects.Models.Model", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ModelTypeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("ProjectIdID")
                        .HasColumnType("int");

                    b.Property<string>("text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ModelTypeID");

                    b.HasIndex("ParentId");

                    b.HasIndex("ProjectIdID");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("WebApiObjects.Models.ModelType", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.ToTable("ModelTypes");
                });

            modelBuilder.Entity("WebApiObjects.Models.Project", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("WebApiObjects.Models.Property", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ModelID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ModelID");

                    b.HasIndex("ParentId");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("WebApiObjects.Models.Type", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ModelTypeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ParentModelTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Typ")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ModelTypeID");

                    b.HasIndex("ParentModelTypeId");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("WebApiObjects.Models.Action", b =>
                {
                    b.HasOne("WebApiObjects.Models.Model", "ParentModel")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.Navigation("ParentModel");
                });

            modelBuilder.Entity("WebApiObjects.Models.Model", b =>
                {
                    b.HasOne("WebApiObjects.Models.ModelType", null)
                        .WithMany("Models")
                        .HasForeignKey("ModelTypeID");

                    b.HasOne("WebApiObjects.Models.Model", "ParentModel")
                        .WithMany("children")
                        .HasForeignKey("ParentId");

                    b.HasOne("WebApiObjects.Models.Project", "ProjectId")
                        .WithMany()
                        .HasForeignKey("ProjectIdID");

                    b.Navigation("ParentModel");

                    b.Navigation("ProjectId");
                });

            modelBuilder.Entity("WebApiObjects.Models.Property", b =>
                {
                    b.HasOne("WebApiObjects.Models.Model", null)
                        .WithMany("Properties")
                        .HasForeignKey("ModelID");

                    b.HasOne("WebApiObjects.Models.Model", "ParentModel")
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentModel");
                });

            modelBuilder.Entity("WebApiObjects.Models.Type", b =>
                {
                    b.HasOne("WebApiObjects.Models.ModelType", null)
                        .WithMany("ModelTypes")
                        .HasForeignKey("ModelTypeID");

                    b.HasOne("WebApiObjects.Models.ModelType", "ParentModelType")
                        .WithMany()
                        .HasForeignKey("ParentModelTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentModelType");
                });

            modelBuilder.Entity("WebApiObjects.Models.Model", b =>
                {
                    b.Navigation("children");

                    b.Navigation("Properties");
                });

            modelBuilder.Entity("WebApiObjects.Models.ModelType", b =>
                {
                    b.Navigation("Models");

                    b.Navigation("ModelTypes");
                });
#pragma warning restore 612, 618
        }
    }
}
