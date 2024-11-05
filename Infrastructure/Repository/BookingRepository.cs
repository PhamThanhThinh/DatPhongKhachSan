using Application.Common.Interfaces;
using Application.Common.Utility;
using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
  public class BookingRepository : Repository<Booking>, IBookingRepository
  {
    private readonly ApplicationDbContext _db;

    public BookingRepository(ApplicationDbContext db) : base(db)
    {
      _db = db;
    }

    public void Update(Booking entity)
    {
      //_db.Update(entity);
      _db.Bookings.Update(entity);
    }

    public void UpdateStatus(int bookingId, string bookingStatus)
    {
      var bookingFromDatabase = _db.Bookings.FirstOrDefault(b => b.Id == bookingId);
      if (bookingFromDatabase != null)
      {
        bookingFromDatabase.Status = bookingStatus;
        if (bookingStatus == UserRoles.StatusCheckedIn)
        {
          bookingFromDatabase.ActualCheckInDate = DateTime.Now;
        }
        if (bookingStatus == UserRoles.StatusCompleted)
        {
          bookingFromDatabase.ActualCheckOutDate = DateTime.Now;
        }
      }

    }

    public void UpdateStripePaymentID(int bookingId, string stripeSessionId, string stripePaymentIntentId)
    {
      var bookingFromDatabase = _db.Bookings.FirstOrDefault(b => b.Id == bookingId);
      if (bookingFromDatabase != null)
      {
        if (!string.IsNullOrEmpty(stripeSessionId))
        {
          bookingFromDatabase.StripeSessionId = stripeSessionId; ;
        }
        if (!string.IsNullOrEmpty(stripePaymentIntentId))
        {
          bookingFromDatabase.StripePaymentIntentId = stripePaymentIntentId;
          bookingFromDatabase.PaymentDate = DateTime.Now;
          bookingFromDatabase.IsPaymentSuccessful = true;
        }
      }
    }
  }
}
