﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiObjects.Models;

namespace WebApiObjects.Migrations
{
    [DbContext(typeof(WebDbContext))]
    [Migration("20210812112602_TableChange")]
    partial class TableChange
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApiObjects.Models.Model", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ModelID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ModelID");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("WebApiObjects.Models.Property", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentModelID")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ParentModelID");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("WebApiObjects.Models.Model", b =>
                {
                    b.HasOne("WebApiObjects.Models.Model", null)
                        .WithMany("SubModel")
                        .HasForeignKey("ModelID");
                });

            modelBuilder.Entity("WebApiObjects.Models.Property", b =>
                {
                    b.HasOne("WebApiObjects.Models.Model", "ParentModel")
                        .WithMany()
                        .HasForeignKey("ParentModelID");

                    b.Navigation("ParentModel");
                });

            modelBuilder.Entity("WebApiObjects.Models.Model", b =>
                {
                    b.Navigation("SubModel");
                });
#pragma warning restore 612, 618
        }
    }
}
