using Application.Common.Interfaces;
using Application.Common.Utility;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace Presentation.Controllers
{
  public class BookingController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;

    public BookingController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    //[Authorize]
    [Authorize(Roles = UserRoles.Role_Customer)]
    public IActionResult FinalizeBooking(int hotelId, DateOnly checkInDate, int nights)
    {
      var thuVienIdentity = (ClaimsIdentity)User.Identity;
      var userId = thuVienIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

      ApplicationUser user = _unitOfWork.ApplicationUser.Get(h => h.Id == userId);

      Booking booking = new()
      {
        HotelId = hotelId,
        Hotel = _unitOfWork.Hotel.Get(h=>h.Id == hotelId, includeProperties: "HotelAmenity"),
        CheckInDate = checkInDate,
        Nights = nights,
        CheckOutDate = checkInDate.AddDays(nights),
        UserId = userId,
        Phone = user.PhoneNumber,
        Email = user.Email,
        Name = user.Name,
      };

      booking.TotalCost = booking.Hotel.Price * nights;

      return View(booking);
    }

    [Authorize(Roles = UserRoles.Role_Customer)]
    [HttpPost]
    public IActionResult FinalizeBooking(Booking booking)
    {
      var hotel = _unitOfWork.Hotel.Get(h => h.Id == booking.HotelId);

      booking.TotalCost = hotel.Price * booking.Nights;
      booking.Status = UserRoles.StatusPending;
      booking.BookingDate = DateTime.Now;

      _unitOfWork.Booking.Add(booking);
      _unitOfWork.Save();

      //return RedirectToAction("BookingConfirmation", "Booking");
      return RedirectToAction(nameof(BookingConfirmation), new {bookingId = booking.Id});
    }

    [Authorize(Roles = UserRoles.Role_Customer)]
    public IActionResult BookingConfirmation(int bookingId)
    {
      return View(bookingId);
    }

  }
}
