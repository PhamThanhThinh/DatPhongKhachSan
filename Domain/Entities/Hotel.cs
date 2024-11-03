using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
  public class Hotel
  {
    public int Id { get; set; }
    [Display(Name = "Tên khách sạn *")]
    [MaxLength(100)]
    public required string Name { get; set; }
    public string? Description { get; set; }

    [Display(Name = "Giá Phòng Một Đêm *")]
    [Range(100, 20000)]
    public double Price { get; set; }

    // số mét vuông của một phòng
    public int SquareMeter { get; set; }

    // số lượng người tối đa trong một phòng
    [Range(1, 20)]
    public int Occupancy { get; set; }

    [NotMapped]
    public IFormFile? Image { get; set; }

    [Display(Name = "Link Hình Ảnh")]
    public string? ImageUrl { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }

    [ValidateNever]
    public IEnumerable<Amenity> HotelAmenity { get; set; }

    [NotMapped]
    public bool IsAvailable { get; set; } = true;
    //public bool IsAvailable { get; set; }

  }
}
