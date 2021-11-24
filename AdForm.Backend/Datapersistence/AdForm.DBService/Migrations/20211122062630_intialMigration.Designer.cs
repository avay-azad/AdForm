﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AdForm.DBService.Migrations
{
    [DbContext(typeof(HomeworkDBContext))]
    [Migration("20211122062630_intialMigration")]
    partial class intialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AdForm.DBService.LabelToDoItem", b =>
                {
                    b.Property<long>("LabelId")
                        .HasColumnType("bigint");

                    b.Property<long>("ToDoItemId")
                        .HasColumnType("bigint");

                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("LabelId", "ToDoItemId");

                    b.HasIndex("ToDoItemId");

                    b.ToTable("LabelToDoItems");
                });

            modelBuilder.Entity("AdForm.DBService.LabelToDoList", b =>
                {
                    b.Property<long>("LabelId")
                        .HasColumnType("bigint");

                    b.Property<long>("ToDoListId")
                        .HasColumnType("bigint");

                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("LabelId", "ToDoListId");

                    b.HasIndex("ToDoListId");

                    b.ToTable("LabelToDoLists");
                });

            modelBuilder.Entity("AdForm.DBService.Labels", b =>
                {
                    b.Property<long>("LabelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("LabelId");

                    b.HasIndex("UserId");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("AdForm.DBService.ToDoItems", b =>
                {
                    b.Property<long>("ToDoItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ToDoListId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("ToDoItemId");

                    b.HasIndex("ToDoListId");

                    b.HasIndex("UserId");

                    b.ToTable("ToDoItems");
                });

            modelBuilder.Entity("AdForm.DBService.ToDoLists", b =>
                {
                    b.Property<long>("ToDoListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("ToDoListId");

                    b.HasIndex("UserId");

                    b.ToTable("ToDoLists");
                });

            modelBuilder.Entity("AdForm.DBService.Users", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1L,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Avay",
                            LastName = "Azad",
                            Password = "peEOLSECrwrx0wHlPWvRpe3xW9dNiqX8sOmBNNbcCdk=",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserName = "avay"
                        },
                        new
                        {
                            UserId = 2L,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Amar",
                            LastName = "kaushik",
                            Password = "Ad9dObo9lv45yafXeZbhvtmUcep0AWa398OP+AqvNng=",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserName = "amar"
                        },
                        new
                        {
                            UserId = 3L,
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Azad",
                            LastName = "Azad",
                            Password = "ITm2Sn8hxDH17QWmvBoBzGebjwIraEgkC7Zah+NHIwo=",
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserName = "azad"
                        });
                });

            modelBuilder.Entity("AdForm.DBService.LabelToDoItem", b =>
                {
                    b.HasOne("AdForm.DBService.Labels", "Label")
                        .WithMany("LabelToDoItems")
                        .HasForeignKey("LabelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AdForm.DBService.ToDoItems", "ToDoItem")
                        .WithMany("LabelToDoItems")
                        .HasForeignKey("ToDoItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Label");

                    b.Navigation("ToDoItem");
                });

            modelBuilder.Entity("AdForm.DBService.LabelToDoList", b =>
                {
                    b.HasOne("AdForm.DBService.Labels", "Label")
                        .WithMany("LabelToDoLists")
                        .HasForeignKey("LabelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AdForm.DBService.ToDoLists", "ToDoList")
                        .WithMany("LabelToDoLists")
                        .HasForeignKey("ToDoListId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Label");

                    b.Navigation("ToDoList");
                });

            modelBuilder.Entity("AdForm.DBService.Labels", b =>
                {
                    b.HasOne("AdForm.DBService.Users", "Users")
                        .WithMany("Labels")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("AdForm.DBService.ToDoItems", b =>
                {
                    b.HasOne("AdForm.DBService.ToDoLists", "ToDoLists")
                        .WithMany("ToDoItems")
                        .HasForeignKey("ToDoListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdForm.DBService.Users", "Users")
                        .WithMany("ToDoItems")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ToDoLists");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("AdForm.DBService.ToDoLists", b =>
                {
                    b.HasOne("AdForm.DBService.Users", "Users")
                        .WithMany("ToDoLists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("AdForm.DBService.Labels", b =>
                {
                    b.Navigation("LabelToDoItems");

                    b.Navigation("LabelToDoLists");
                });

            modelBuilder.Entity("AdForm.DBService.ToDoItems", b =>
                {
                    b.Navigation("LabelToDoItems");
                });

            modelBuilder.Entity("AdForm.DBService.ToDoLists", b =>
                {
                    b.Navigation("LabelToDoLists");

                    b.Navigation("ToDoItems");
                });

            modelBuilder.Entity("AdForm.DBService.Users", b =>
                {
                    b.Navigation("Labels");

                    b.Navigation("ToDoItems");

                    b.Navigation("ToDoLists");
                });
#pragma warning restore 612, 618
        }
    }
}
