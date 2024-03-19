﻿// <auto-generated />
using System;
using HelixLaserWorks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HelixLaserWorks.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240316134400_PartExtended")]
    partial class PartExtended
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HelixLaserWorks.Infrastructure.Data.Models.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("CorrosionResistance")
                        .HasColumnType("bit");

                    b.Property<double>("Density")
                        .HasColumnType("float");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(800)
                        .HasColumnType("nvarchar(800)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaterialTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("PricePerSquareMeter")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Specification")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("MaterialTypeId");

                    b.ToTable("Materials");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CorrosionResistance = false,
                            Density = 7.7999999999999998,
                            Description = "Mild steel is a type of low-carbon steel that posesses a relatively low level of carbon content. This makes it malleable, ductile, and easy to weld. Our mild steel is the go-to choice for fabricators, welders, machinists, prototypers, and anyone else who needs a strong, reliable part that is easy to work with.",
                            ImageUrl = "https://i.ebayimg.com/images/g/-yEAAOSwMvpb0FqE/s-l1600.jpg",
                            MaterialTypeId = 1,
                            Name = "MildSteel37",
                            PricePerSquareMeter = 10.99m,
                            Specification = "Cold Rolled"
                        },
                        new
                        {
                            Id = 2,
                            CorrosionResistance = true,
                            Density = 7.9000000000000004,
                            Description = "Weldable, formable, and easy to work with, 304 stainless is our first choice for projects that require massive strength and durability. Laser cut 304 stainless steel is oxidation resistant, making it easy to sanitize and maintain. This particular feature makes 304 stainless steel the go-to grade for many food service applications, from countertops to cookware.",
                            ImageUrl = "https://www.smetals.co.uk/wp-content/uploads/2023/04/Stainless-Steel-304-Grade-0.5mm-Panels-Image-2.jpg",
                            MaterialTypeId = 1,
                            Name = "StainlessSteel304",
                            PricePerSquareMeter = 29.00m
                        },
                        new
                        {
                            Id = 3,
                            CorrosionResistance = true,
                            Density = 2.6800000000000002,
                            Description = "5052 H32 aluminum is strong, inexpensive, and lightweight. Whether you’re welding, machining, or bending, 5052 aluminum is going to be the go-to material for those projects that need excellent all-around material properties. Our laser cut aluminum is exceptionally lightweight and strong, making it perfect for projects where overall load is a concern.",
                            ImageUrl = "https://res.cloudinary.com/rsc/image/upload/bo_1.5px_solid_white,b_auto,c_pad,dpr_2,f_auto,h_399,q_auto,w_710/c_pad,h_399,w_710/F0434043-01?pgw=1",
                            MaterialTypeId = 1,
                            Name = "AluminumH32",
                            PricePerSquareMeter = 32.00m
                        },
                        new
                        {
                            Id = 4,
                            CorrosionResistance = true,
                            Density = 8.9600000000000009,
                            Description = "Our C110 half-hard copper is classified as electrolytic copper, which basically means it’s an extremely high purity (greater than 99% copper, ours is 99.9%). For your projects, this means that the material’s electrical properties won’t be hampered by any erroneous leftover elements. You’re getting one of the purest grades available.",
                            ImageUrl = "https://www.artisansupplies.com.au/wp-content/uploads/2015/11/copper.jpg",
                            MaterialTypeId = 1,
                            Name = "Copper",
                            PricePerSquareMeter = 42.00m
                        },
                        new
                        {
                            Id = 5,
                            CorrosionResistance = true,
                            Density = 0.63,
                            Description = "Chipboard, also known as particleboard, is an engineered wood product made from wood chips, shavings, and resin that are compressed and bonded together under heat and pressure. It’s commonly used in furniture, shelving, and construction applications as an affordable alternative to solid wood, offering good stability but with a slightly coarser texture. One of laser cut Chipboard’s greatest assets is that it’s completely green. Made entirely from recycled pasteboard, our chipboard is 100% recyclable.",
                            ImageUrl = "https://media.wickes.co.uk/is/image/wickes/normal/Chipboard-Flooring-Wickes-P5-T-G-Chipboard-Flooring-18mm-x-600mm-x-2-4m~N0705_164516_00?$ratio43$&fit=crop&extend=-50,-250,-50,0",
                            MaterialTypeId = 3,
                            Name = "ChipWood",
                            PricePerSquareMeter = 6.00m
                        });
                });

            modelBuilder.Entity("HelixLaserWorks.Infrastructure.Data.Models.MaterialThickness", b =>
                {
                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<int>("ThicknessId")
                        .HasColumnType("int");

                    b.HasKey("MaterialId", "ThicknessId");

                    b.HasIndex("ThicknessId");

                    b.ToTable("MaterialsThicknesses");

                    b.HasData(
                        new
                        {
                            MaterialId = 1,
                            ThicknessId = 1
                        },
                        new
                        {
                            MaterialId = 1,
                            ThicknessId = 2
                        },
                        new
                        {
                            MaterialId = 1,
                            ThicknessId = 3
                        },
                        new
                        {
                            MaterialId = 1,
                            ThicknessId = 4
                        },
                        new
                        {
                            MaterialId = 1,
                            ThicknessId = 5
                        },
                        new
                        {
                            MaterialId = 1,
                            ThicknessId = 6
                        },
                        new
                        {
                            MaterialId = 1,
                            ThicknessId = 7
                        },
                        new
                        {
                            MaterialId = 1,
                            ThicknessId = 8
                        },
                        new
                        {
                            MaterialId = 1,
                            ThicknessId = 9
                        },
                        new
                        {
                            MaterialId = 1,
                            ThicknessId = 10
                        },
                        new
                        {
                            MaterialId = 1,
                            ThicknessId = 11
                        },
                        new
                        {
                            MaterialId = 2,
                            ThicknessId = 1
                        },
                        new
                        {
                            MaterialId = 2,
                            ThicknessId = 2
                        },
                        new
                        {
                            MaterialId = 2,
                            ThicknessId = 3
                        },
                        new
                        {
                            MaterialId = 2,
                            ThicknessId = 4
                        },
                        new
                        {
                            MaterialId = 2,
                            ThicknessId = 5
                        },
                        new
                        {
                            MaterialId = 2,
                            ThicknessId = 6
                        },
                        new
                        {
                            MaterialId = 2,
                            ThicknessId = 7
                        },
                        new
                        {
                            MaterialId = 2,
                            ThicknessId = 8
                        },
                        new
                        {
                            MaterialId = 3,
                            ThicknessId = 1
                        },
                        new
                        {
                            MaterialId = 3,
                            ThicknessId = 2
                        },
                        new
                        {
                            MaterialId = 3,
                            ThicknessId = 3
                        },
                        new
                        {
                            MaterialId = 3,
                            ThicknessId = 4
                        },
                        new
                        {
                            MaterialId = 4,
                            ThicknessId = 1
                        },
                        new
                        {
                            MaterialId = 4,
                            ThicknessId = 2
                        },
                        new
                        {
                            MaterialId = 4,
                            ThicknessId = 3
                        },
                        new
                        {
                            MaterialId = 4,
                            ThicknessId = 4
                        },
                        new
                        {
                            MaterialId = 5,
                            ThicknessId = 1
                        },
                        new
                        {
                            MaterialId = 5,
                            ThicknessId = 2
                        },
                        new
                        {
                            MaterialId = 5,
                            ThicknessId = 3
                        },
                        new
                        {
                            MaterialId = 5,
                            ThicknessId = 4
                        },
                        new
                        {
                            MaterialId = 5,
                            ThicknessId = 5
                        },
                        new
                        {
                            MaterialId = 5,
                            ThicknessId = 6
                        },
                        new
                        {
                            MaterialId = 5,
                            ThicknessId = 7
                        },
                        new
                        {
                            MaterialId = 5,
                            ThicknessId = 8
                        },
                        new
                        {
                            MaterialId = 5,
                            ThicknessId = 9
                        },
                        new
                        {
                            MaterialId = 5,
                            ThicknessId = 10
                        },
                        new
                        {
                            MaterialId = 5,
                            ThicknessId = 11
                        });
                });

            modelBuilder.Entity("HelixLaserWorks.Infrastructure.Data.Models.MaterialType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("MaterialType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Metal"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Plastic"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Wood"
                        });
                });

            modelBuilder.Entity("HelixLaserWorks.Infrastructure.Data.Models.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeliveryDueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("HelixLaserWorks.Infrastructure.Data.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("OfferId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OfferId")
                        .IsUnique()
                        .HasFilter("[OfferId] IS NOT NULL");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("HelixLaserWorks.Infrastructure.Data.Models.Part", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("SchemeURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Thickness")
                        .HasColumnType("float");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("MaterialId");

                    b.HasIndex("OrderId");

                    b.ToTable("Parts");
                });

            modelBuilder.Entity("HelixLaserWorks.Infrastructure.Data.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Comment")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("HelixLaserWorks.Infrastructure.Data.Models.Thickness", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Thicknesses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Value = 1.0
                        },
                        new
                        {
                            Id = 2,
                            Value = 2.0
                        },
                        new
                        {
                            Id = 3,
                            Value = 3.0
                        },
                        new
                        {
                            Id = 4,
                            Value = 4.0
                        },
                        new
                        {
                            Id = 5,
                            Value = 5.0
                        },
                        new
                        {
                            Id = 6,
                            Value = 6.0
                        },
                        new
                        {
                            Id = 7,
                            Value = 8.0
                        },
                        new
                        {
                            Id = 8,
                            Value = 10.0
                        },
                        new
                        {
                            Id = 9,
                            Value = 12.0
                        },
                        new
                        {
                            Id = 10,
                            Value = 15.0
                        },
                        new
                        {
                            Id = 11,
                            Value = 20.0
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("HelixLaserWorks.Infrastructure.Data.Models.Material", b =>
                {
                    b.HasOne("HelixLaserWorks.Infrastructure.Data.Models.MaterialType", "MaterialType")
                        .WithMany()
                        .HasForeignKey("MaterialTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MaterialType");
                });

            modelBuilder.Entity("HelixLaserWorks.Infrastructure.Data.Models.MaterialThickness", b =>
                {
                    b.HasOne("HelixLaserWorks.Infrastructure.Data.Models.Material", "Material")
                        .WithMany("MaterialThicknesses")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelixLaserWorks.Infrastructure.Data.Models.Thickness", "Thickness")
                        .WithMany("MaterialThicknesses")
                        .HasForeignKey("ThicknessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Material");

                    b.Navigation("Thickness");
                });

            modelBuilder.Entity("HelixLaserWorks.Infrastructure.Data.Models.Order", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelixLaserWorks.Infrastructure.Data.Models.Offer", "Offer")
                        .WithOne("Order")
                        .HasForeignKey("HelixLaserWorks.Infrastructure.Data.Models.Order", "OfferId");

                    b.Navigation("Customer");

                    b.Navigation("Offer");
                });

            modelBuilder.Entity("HelixLaserWorks.Infrastructure.Data.Models.Part", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HelixLaserWorks.Infrastructure.Data.Models.Material", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelixLaserWorks.Infrastructure.Data.Models.Order", null)
                        .WithMany("Parts")
                        .HasForeignKey("OrderId");

                    b.Navigation("Creator");

                    b.Navigation("Material");
                });

            modelBuilder.Entity("HelixLaserWorks.Infrastructure.Data.Models.Review", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HelixLaserWorks.Infrastructure.Data.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HelixLaserWorks.Infrastructure.Data.Models.Material", b =>
                {
                    b.Navigation("MaterialThicknesses");
                });

            modelBuilder.Entity("HelixLaserWorks.Infrastructure.Data.Models.Offer", b =>
                {
                    b.Navigation("Order")
                        .IsRequired();
                });

            modelBuilder.Entity("HelixLaserWorks.Infrastructure.Data.Models.Order", b =>
                {
                    b.Navigation("Parts");
                });

            modelBuilder.Entity("HelixLaserWorks.Infrastructure.Data.Models.Thickness", b =>
                {
                    b.Navigation("MaterialThicknesses");
                });
#pragma warning restore 612, 618
        }
    }
}