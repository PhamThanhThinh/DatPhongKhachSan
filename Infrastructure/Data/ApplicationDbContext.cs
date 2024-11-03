using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<HotelNumber> HotelNumbers { get; set; }
    //public DbSet<User> Users { get; set; }
    //public DbSet<Booking> Bookings { get; set; }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public DbSet<Amenity> Amenities { get; set; }

    public DbSet<Booking> Bookings { get; set; }

    // thêm data vào database bằng code
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<Hotel>().HasData(
        new Hotel
        {
          Id = 1,
          Name = "Khach San 5 Sao",
          Description = "vip pro",
          ImageUrl = "https://d2e5ushqwiltxm.cloudfront.net/wp-content/uploads/sites/48/2024/07/05063252/PVD_DJI_0519_FULLSIZE-TIFF-AdobeRGB-17.jpg",
          Occupancy = 20,
          Price = 1000,
          SquareMeter = 1000,
        },
        new Hotel
        {
          Id = 2,
          Name = "Khach San 5 Sao",
          Description = "vip pro",
          ImageUrl = "https://d2e5ushqwiltxm.cloudfront.net/wp-content/uploads/sites/48/2024/07/05063252/PVD_DJI_0519_FULLSIZE-TIFF-AdobeRGB-17.jpg",
          Occupancy = 20,
          Price = 1000,
          SquareMeter = 1000,
        },
        new Hotel
        {
          Id = 3,
          Name = "Khach San 5 Sao",
          Description = "vip pro",
          ImageUrl = "https://d2e5ushqwiltxm.cloudfront.net/wp-content/uploads/sites/48/2024/07/05063252/PVD_DJI_0519_FULLSIZE-TIFF-AdobeRGB-17.jpg",
          Occupancy = 20,
          Price = 1000,
          SquareMeter = 1000,
        },
        new Hotel
        {
          Id = 4,
          Name = "Khach San 5 Sao",
          Description = "vip pro",
          ImageUrl = "https://d2e5ushqwiltxm.cloudfront.net/wp-content/uploads/sites/48/2024/07/05063252/PVD_DJI_0519_FULLSIZE-TIFF-AdobeRGB-17.jpg",
          Occupancy = 20,
          Price = 1000,
          SquareMeter = 1000,
        }
      );

      modelBuilder.Entity<HotelNumber>().HasData(
        new HotelNumber
        {
          Hotel_Number = 202,
          HotelId = 13,
        },
        new HotelNumber
        {
          Hotel_Number = 203,
          HotelId = 13,
        },
        new HotelNumber
        {
          Hotel_Number = 204,
          HotelId = 13,
        },
        new HotelNumber
        {
          Hotel_Number = 205,
          HotelId = 13,
        }
        );

      modelBuilder.Entity<Amenity>().HasData(
        new Amenity
        {
          Id = 1,
          HotelId = 15,
          Name = "vip pro 5 sao"
        },
        new Amenity
        {
          Id = 2,
          HotelId = 15,
          Name = "vip pro 5 sao đệ nhị"
        },
        new Amenity
        {
          Id = 3,
          HotelId = 15,
          Name = "vip pro 5 sao đệ tam"
        }
        );
    }

  }

}
