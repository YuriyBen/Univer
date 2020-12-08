﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Univer.DAL;

namespace Univer.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20201126123943_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Univer.DAL.Entities.Image", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<byte[]>("Data")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("UserPublicDataId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserPublicDataId")
                        .IsUnique();

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Univer.DAL.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Univer.DAL.Entities.UserPublicData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("WantsToRecieveNotifications")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UsersPublicData");
                });

            modelBuilder.Entity("Univer.DAL.Entities.Image", b =>
                {
                    b.HasOne("Univer.DAL.Entities.UserPublicData", "UserPublicData")
                        .WithOne("ProfileImage")
                        .HasForeignKey("Univer.DAL.Entities.Image", "UserPublicDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserPublicData");
                });

            modelBuilder.Entity("Univer.DAL.Entities.UserPublicData", b =>
                {
                    b.HasOne("Univer.DAL.Entities.User", "User")
                        .WithOne("UserPublicData")
                        .HasForeignKey("Univer.DAL.Entities.UserPublicData", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Univer.DAL.Entities.User", b =>
                {
                    b.Navigation("UserPublicData");
                });

            modelBuilder.Entity("Univer.DAL.Entities.UserPublicData", b =>
                {
                    b.Navigation("ProfileImage");
                });
#pragma warning restore 612, 618
        }
    }
}
