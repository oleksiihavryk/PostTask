﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PostTask.RestService.Data;

#nullable disable

namespace PostTask.RestService.Data.Migrations
{
    [DbContext(typeof(PostTaskDatabaseContext))]
    [Migration("20221025111202_TaskStateFix")]
    partial class TaskStateFix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PostTask.RestService.Domain.GroupFolder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TaskGroupFolder", (string)null);
                });

            modelBuilder.Entity("PostTask.RestService.Domain.State", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("ItemState", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("State");
                });

            modelBuilder.Entity("PostTask.RestService.Domain.Task", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<Guid?>("StateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TaskGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.HasIndex("TaskGroupId");

                    b.ToTable("Task", (string)null);
                });

            modelBuilder.Entity("PostTask.RestService.Domain.TaskGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid?>("GroupFolderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GroupFolderId");

                    b.ToTable("TaskGroupDto", (string)null);
                });

            modelBuilder.Entity("PostTask.RestService.Domain.TaskItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("From")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDone")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<Guid?>("TaskGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("To")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TaskGroupId");

                    b.HasIndex("TaskId");

                    b.ToTable("TaskItem", (string)null);
                });

            modelBuilder.Entity("PostTask.RestService.Domain.UserState", b =>
                {
                    b.HasBaseType("PostTask.RestService.Domain.State");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("UserState");
                });

            modelBuilder.Entity("PostTask.RestService.Domain.Task", b =>
                {
                    b.HasOne("PostTask.RestService.Domain.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId");

                    b.HasOne("PostTask.RestService.Domain.TaskGroup", null)
                        .WithMany()
                        .HasForeignKey("TaskGroupId");

                    b.Navigation("State");
                });

            modelBuilder.Entity("PostTask.RestService.Domain.TaskGroup", b =>
                {
                    b.HasOne("PostTask.RestService.Domain.GroupFolder", null)
                        .WithMany("Items")
                        .HasForeignKey("GroupFolderId");
                });

            modelBuilder.Entity("PostTask.RestService.Domain.TaskItem", b =>
                {
                    b.HasOne("PostTask.RestService.Domain.TaskGroup", null)
                        .WithMany("Items")
                        .HasForeignKey("TaskGroupId");

                    b.HasOne("PostTask.RestService.Domain.Task", null)
                        .WithMany("Items")
                        .HasForeignKey("TaskId")
                        .IsRequired();
                });

            modelBuilder.Entity("PostTask.RestService.Domain.GroupFolder", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("PostTask.RestService.Domain.Task", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("PostTask.RestService.Domain.TaskGroup", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
