﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241024145217_mota-themtruongAmenity")]
    partial class motathemtruongAmenity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Amenity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("Amenities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            HotelId = 15,
                            Name = "vip pro 5 sao"
                        },
                        new
                        {
                            Id = 2,
                            HotelId = 15,
                            Name = "vip pro 5 sao đệ nhị"
                        },
                        new
                        {
                            Id = 3,
                            HotelId = 15,
                            Name = "vip pro 5 sao đệ tam"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Occupancy")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("SquareMeter")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Hotels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "vip pro",
                            ImageUrl = "https://d2e5ushqwiltxm.cloudfront.net/wp-content/uploads/sites/48/2024/07/05063252/PVD_DJI_0519_FULLSIZE-TIFF-AdobeRGB-17.jpg",
                            Name = "Khach San 5 Sao",
                            Occupancy = 20,
                            Price = 1000.0,
                            SquareMeter = 1000
                        },
                        new
                        {
                            Id = 2,
                            Description = "vip pro",
                            ImageUrl = "https://d2e5ushqwiltxm.cloudfront.net/wp-content/uploads/sites/48/2024/07/05063252/PVD_DJI_0519_FULLSIZE-TIFF-AdobeRGB-17.jpg",
                            Name = "Khach San 5 Sao",
                            Occupancy = 20,
                            Price = 1000.0,
                            SquareMeter = 1000
                        },
                        new
                        {
                            Id = 3,
                            Description = "vip pro",
                            ImageUrl = "https://d2e5ushqwiltxm.cloudfront.net/wp-content/uploads/sites/48/2024/07/05063252/PVD_DJI_0519_FULLSIZE-TIFF-AdobeRGB-17.jpg",
                            Name = "Khach San 5 Sao",
                            Occupancy = 20,
                            Price = 1000.0,
                            SquareMeter = 1000
                        },
                        new
                        {
                            Id = 4,
                            Description = "vip pro",
                            ImageUrl = "https://d2e5ushqwiltxm.cloudfront.net/wp-content/uploads/sites/48/2024/07/05063252/PVD_DJI_0519_FULLSIZE-TIFF-AdobeRGB-17.jpg",
                            Name = "Khach San 5 Sao",
                            Occupancy = 20,
                            Price = 1000.0,
                            SquareMeter = 1000
                        });
                });

            modelBuilder.Entity("Domain.Entities.HotelNumber", b =>
                {
                    b.Property<int>("Hotel_Number")
                        .HasColumnType("int");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("SpecialDetails")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Hotel_Number");

                    b.HasIndex("HotelId");

                    b.ToTable("HotelNumbers");

                    b.HasData(
                        new
                        {
                            Hotel_Number = 202,
                            HotelId = 13
                        },
                        new
                        {
                            Hotel_Number = 203,
                            HotelId = 13
                        },
                        new
                        {
                            Hotel_Number = 204,
                            HotelId = 13
                        },
                        new
                        {
                            Hotel_Number = 205,
                            HotelId = 13
                        });
                });

            modelBuilder.Entity("Domain.Entities.Amenity", b =>
                {
                    b.HasOne("Domain.Entities.Hotel", "Hotel")
                        .WithMany()
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("Domain.Entities.HotelNumber", b =>
                {
                    b.HasOne("Domain.Entities.Hotel", "Hotel")
                        .WithMany()
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });
#pragma warning restore 612, 618
        }
    }
}
