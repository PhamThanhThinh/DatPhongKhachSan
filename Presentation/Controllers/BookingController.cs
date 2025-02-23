using Application.Common.Interfaces;
using Application.Common.Utility;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.BillingPortal;
using Stripe.Checkout;
using System.Net.WebSockets;
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

    public IActionResult Index()
    {
      return View();
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
        Hotel = _unitOfWork.Hotel.Get(h => h.Id == hotelId, includeProperties: "HotelAmenity"),
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

    // hoàn thành thanh toán
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

      var domain = Request.Scheme + "://" + Request.Host.Value + "/";
      var options = new Stripe.Checkout.SessionCreateOptions
      {
        LineItems = new List<SessionLineItemOptions>(),
        Mode = "payment",
        SuccessUrl = domain + $"/booking/BookingConfirmation?bookingId={booking.Id}",
        CancelUrl = domain + $"/booking/FinalizeBooking?hotelId={booking.HotelId}&checkInDate={booking.CheckInDate}&nights={booking.Nights}",
      };

      options.LineItems.Add(new SessionLineItemOptions
      {
        PriceData = new SessionLineItemPriceDataOptions
        {
          UnitAmount = 2000, // Amount in cents
          //UnitAmount = (long)(booking.TotalCost + VAT),
          Currency = "usd",
          ProductData = new SessionLineItemPriceDataProductDataOptions
          {
            //Name = "Tên Khách Sạn",
            Name = hotel.Name,
          },
        },
        Quantity = 1,
      });


      //LineItems = new List<SessionLineItemOptions>
      //  {
      //    new SessionLineItemOptions
      //    {
      //      PriceData = new SessionLineItemPriceDataOptions
      //      {
      //        UnitAmount = 2000, // Amount in cents
      //        Currency = "usd",
      //        ProductData = new SessionLineItemPriceDataProductDataOptions
      //        {
      //            Name = "Sample Product",
      //        },
      //      },
      //      Quantity = 1,
      //    },
      //  },

      //var service = new SessionService();
      //Session session = service.Create(options);

      //return Json(new { id = session.Id });

      //return RedirectToAction("BookingConfirmation", "Booking");
      //return RedirectToAction(nameof(BookingConfirmation), new {bookingId = booking.Id});

      var service = new Stripe.Checkout.SessionService();
      Stripe.Checkout.Session session = service.Create(options);

      _unitOfWork.Booking.UpdateStripePaymentID(booking.Id, session.Id, session.PaymentIntentId);

      _unitOfWork.Save();

      Response.Headers.Add("Location", session.Url);

      //return RedirectToAction(nameof(BookingConfirmation), new { bookingId = booking.Id });
      return new StatusCodeResult(303);
    }

    [Authorize(Roles = UserRoles.Role_Customer)]
    public IActionResult BookingConfirmation(int bookingId)
    {
      return View(bookingId);
    }

    #region Gọi API
    [HttpGet]
    //[Authorize]
    public IActionResult GetAll()
    {
      IEnumerable<Booking> objBookings;

      if (User.IsInRole(UserRoles.Role_Admin))
      {
        objBookings = _unitOfWork.Booking.GetAll(includeProperties: "User,Hotel");
      }
      else
      {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        objBookings = _unitOfWork.Booking.GetAll(u => u.UserId == userId, includeProperties: "User,Hotel");

      }
      //objBookings = _unitOfWork.Booking.GetAll(includeProperties: "ApplicationUser,Hotel");
      return Json(new { data = objBookings });

    }
    #endregion


  }
}
