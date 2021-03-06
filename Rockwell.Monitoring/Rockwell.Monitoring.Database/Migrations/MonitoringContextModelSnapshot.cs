// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rockwell.Monitoring.Database;

namespace Rockwell.Monitoring.Database.Migrations
{
    [DbContext(typeof(MonitoringContext))]
    partial class MonitoringContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("Rockwell.Monitoring.Database.Entities.ExecutionResult", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("ErrorMessage")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime>("ExecutionTime")
                        .HasColumnType("datetime");

                    b.Property<int>("ResponseCode")
                        .HasColumnType("int");

                    b.Property<string>("Result")
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<long>("ScrapeJobId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ScrapeJobId");

                    b.ToTable("execution-results");
                });

            modelBuilder.Entity("Rockwell.Monitoring.Database.Entities.ScrapeJob", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime");

                    b.Property<string>("CronExpression")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("Url", "CronExpression")
                        .IsUnique();

                    b.ToTable("scrape-jobs");
                });

            modelBuilder.Entity("Rockwell.Monitoring.Database.Entities.ExecutionResult", b =>
                {
                    b.HasOne("Rockwell.Monitoring.Database.Entities.ScrapeJob", "ScrapeJob")
                        .WithMany("ExecutionResults")
                        .HasForeignKey("ScrapeJobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ScrapeJob");
                });

            modelBuilder.Entity("Rockwell.Monitoring.Database.Entities.ScrapeJob", b =>
                {
                    b.Navigation("ExecutionResults");
                });
#pragma warning restore 612, 618
        }
    }
}
