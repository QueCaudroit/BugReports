﻿// <auto-generated />
using System;
using BugReportModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BugReportModule.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200223152618_AddBugReportFiles")]
    partial class AddBugReportFiles
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("BugReportModule.BugReport", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BugDescription")
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Logs")
                        .HasColumnType("text");

                    b.Property<int>("PlayerID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.ToTable("BugReports");
                });

            modelBuilder.Entity("BugReportModule.BugReportFile", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BugReportID")
                        .HasColumnType("uuid");

                    b.Property<string>("Filename")
                        .HasColumnType("text");

                    b.Property<string>("Path")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("BugReportID");

                    b.ToTable("BugReportFiles");
                });

            modelBuilder.Entity("BugReportModule.BugReportFile", b =>
                {
                    b.HasOne("BugReportModule.BugReport", "BugReport")
                        .WithMany("BugReportFiles")
                        .HasForeignKey("BugReportID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
