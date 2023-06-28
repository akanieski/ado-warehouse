﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace WorkItemProcessor.Data.Migrations
{
    [DbContext(typeof(WorkItemProcessorDbContext))]
    partial class WorkItemProcessorDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Descriptor")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "descriptor");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "displayName");

                    b.Property<string>("UniqueId")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<string>("UniqueName")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "uniqueName");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasAnnotation("Relational:JsonPropertyName", "System.CreatedBy");
                });

            modelBuilder.Entity("WorkItemRevision", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("AreaPath")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "System.AreaPath");

                    b.Property<long?>("ChangedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("ChangedDate")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "System.ChangedDate");

                    b.Property<long?>("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "System.CreatedDate");

                    b.Property<string>("IterationPath")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "System.IterationPath");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "System.Reason");

                    b.Property<string>("Severity")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "Microsoft.VSTS.Common.Severity");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "System.State");

                    b.Property<string>("TeamProject")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "System.TeamProject");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "System.Title");

                    b.Property<string>("WorkItemType")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "System.WorkItemType");

                    b.HasKey("Id");

                    b.HasIndex("ChangedById");

                    b.HasIndex("CreatedById");

                    b.ToTable("WorkItemRevisions");
                });

            modelBuilder.Entity("WorkItemRevision", b =>
                {
                    b.HasOne("User", "ChangedBy")
                        .WithMany()
                        .HasForeignKey("ChangedById");

                    b.HasOne("User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.Navigation("ChangedBy");

                    b.Navigation("CreatedBy");
                });
#pragma warning restore 612, 618
        }
    }
}