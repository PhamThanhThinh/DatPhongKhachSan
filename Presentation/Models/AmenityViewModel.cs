using Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.Models
{
  public class AmenityViewModel
  {
    public Amenity? Amenity { get; set; }
    [ValidateNever]
    public IEnumerable<SelectListItem>? HotelList { get; set; }
  }
}
