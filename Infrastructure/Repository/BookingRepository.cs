using Application.Common.Interfaces;
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

    public void UpdateStatus(int bookingId, string orderStatus)
    {
      var bookingFromDatabase = _db.Bookings.FirstOrDefault(b => b.Id == bookingId);
      if (bookingFromDatabase != null)
      {
        bookingFromDatabase.Status = orderStatus;
        _db.SaveChanges();
      }

    }

    public void UpdateStripePaymentID(int bookingId, string stripeSessionId, string stripePaymentIntentId)
    {
      throw new NotImplementedException();
    }
  }
}
