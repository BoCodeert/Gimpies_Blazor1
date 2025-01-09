﻿// <auto-generated />
using System;
using Gimpies_Blazor1.Database.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gimpies_Blazor1.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250108152607_ImageForShoe2")]
    partial class ImageForShoe2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Gimpies_Blazor1.Database.Models.Entities.Brand", b =>
                {
                    b.Property<int>("BrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BrandId"));

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BrandId");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("Gimpies_Blazor1.Database.Models.Entities.Colour", b =>
                {
                    b.Property<int>("ColourId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ColourId"));

                    b.Property<string>("ColourName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ColourId");

                    b.ToTable("Colours");
                });

            modelBuilder.Entity("Gimpies_Blazor1.Database.Models.Entities.Model", b =>
                {
                    b.Property<int>("ModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ModelId"));

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ModelId");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("Gimpies_Blazor1.Database.Models.Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            RoleName = "Admin"
                        },
                        new
                        {
                            RoleId = 2,
                            RoleName = "Buyer"
                        },
                        new
                        {
                            RoleId = 3,
                            RoleName = "Seller"
                        });
                });

            modelBuilder.Entity("Gimpies_Blazor1.Database.Models.Entities.SalesTransaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ShoeId")
                        .HasColumnType("int");

                    b.HasKey("TransactionId");

                    b.HasIndex("ShoeId");

                    b.ToTable("SalesTransactions");
                });

            modelBuilder.Entity("Gimpies_Blazor1.Database.Models.Entities.Shoe", b =>
                {
                    b.Property<int>("ShoeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShoeId"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("ColourId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<int>("SizeId")
                        .HasColumnType("int");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("ShoeId");

                    b.HasIndex("BrandId");

                    b.HasIndex("ColourId");

                    b.HasIndex("ModelId");

                    b.HasIndex("SizeId");

                    b.ToTable("Shoes");
                });

            modelBuilder.Entity("Gimpies_Blazor1.Database.Models.Entities.Size", b =>
                {
                    b.Property<int>("SizeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SizeId"));

                    b.Property<decimal>("SizeValue")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("SizeId");

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("Gimpies_Blazor1.Database.Models.Entities.User", b =>
                {
                    b.Property<int>("Userid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Userid"));

                    b.Property<string>("PasswordHashed")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("fk_UserRoleID")
                        .HasColumnType("int");

                    b.HasKey("Userid");

                    b.HasIndex("fk_UserRoleID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Userid = 1,
                            PasswordHashed = "1",
                            Username = "admin",
                            fk_UserRoleID = 1
                        },
                        new
                        {
                            Userid = 2,
                            PasswordHashed = "1",
                            Username = "buyer",
                            fk_UserRoleID = 2
                        },
                        new
                        {
                            Userid = 3,
                            PasswordHashed = "1",
                            Username = "seller",
                            fk_UserRoleID = 3
                        });
                });

            modelBuilder.Entity("Gimpies_Blazor1.Database.Models.Entities.UserPolicy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("PolicyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("fk_UserRoleID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("fk_UserRoleID");

                    b.ToTable("UserPolicies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsEnabled = true,
                            PolicyName = "View_Shoes",
                            fk_UserRoleID = 1
                        },
                        new
                        {
                            Id = 2,
                            IsEnabled = true,
                            PolicyName = "Add_Shoes",
                            fk_UserRoleID = 1
                        },
                        new
                        {
                            Id = 3,
                            IsEnabled = true,
                            PolicyName = "Edit_Shoes",
                            fk_UserRoleID = 1
                        },
                        new
                        {
                            Id = 4,
                            IsEnabled = true,
                            PolicyName = "Delete_Shoes",
                            fk_UserRoleID = 1
                        },
                        new
                        {
                            Id = 5,
                            IsEnabled = true,
                            PolicyName = "View_Users",
                            fk_UserRoleID = 1
                        },
                        new
                        {
                            Id = 6,
                            IsEnabled = true,
                            PolicyName = "Add_Users",
                            fk_UserRoleID = 1
                        },
                        new
                        {
                            Id = 7,
                            IsEnabled = true,
                            PolicyName = "Edit_Users",
                            fk_UserRoleID = 1
                        },
                        new
                        {
                            Id = 8,
                            IsEnabled = true,
                            PolicyName = "Delete_Users",
                            fk_UserRoleID = 1
                        },
                        new
                        {
                            Id = 9,
                            IsEnabled = true,
                            PolicyName = "View_Shoes",
                            fk_UserRoleID = 2
                        },
                        new
                        {
                            Id = 10,
                            IsEnabled = true,
                            PolicyName = "Buy_Shoes",
                            fk_UserRoleID = 2
                        },
                        new
                        {
                            Id = 11,
                            IsEnabled = true,
                            PolicyName = "View_Shoes",
                            fk_UserRoleID = 3
                        },
                        new
                        {
                            Id = 12,
                            IsEnabled = true,
                            PolicyName = "Sell_Shoes",
                            fk_UserRoleID = 3
                        });
                });

            modelBuilder.Entity("Gimpies_Blazor1.Database.Models.Entities.SalesTransaction", b =>
                {
                    b.HasOne("Gimpies_Blazor1.Database.Models.Entities.Shoe", "Shoe")
                        .WithMany("SalesTransactions")
                        .HasForeignKey("ShoeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shoe");
                });

            modelBuilder.Entity("Gimpies_Blazor1.Database.Models.Entities.Shoe", b =>
                {
                    b.HasOne("Gimpies_Blazor1.Database.Models.Entities.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gimpies_Blazor1.Database.Models.Entities.Colour", "Colour")
                        .WithMany()
                        .HasForeignKey("ColourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gimpies_Blazor1.Database.Models.Entities.Model", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gimpies_Blazor1.Database.Models.Entities.Size", "Size")
                        .WithMany()
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Colour");

                    b.Navigation("Model");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("Gimpies_Blazor1.Database.Models.Entities.User", b =>
                {
                    b.HasOne("Gimpies_Blazor1.Database.Models.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("fk_UserRoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Gimpies_Blazor1.Database.Models.Entities.UserPolicy", b =>
                {
                    b.HasOne("Gimpies_Blazor1.Database.Models.Entities.Role", "Role")
                        .WithMany("Policies")
                        .HasForeignKey("fk_UserRoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Gimpies_Blazor1.Database.Models.Entities.Role", b =>
                {
                    b.Navigation("Policies");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Gimpies_Blazor1.Database.Models.Entities.Shoe", b =>
                {
                    b.Navigation("SalesTransactions");
                });
#pragma warning restore 612, 618
        }
    }
}
