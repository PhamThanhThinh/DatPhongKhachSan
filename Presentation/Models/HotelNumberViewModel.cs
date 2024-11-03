using Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.Models
{
  public class HotelNumberViewModel
  {
    public HotelNumber? HotelNumber { get; set; }
    [ValidateNever]
    public IEnumerable<SelectListItem>? HotelList { get; set; }
  }
}
