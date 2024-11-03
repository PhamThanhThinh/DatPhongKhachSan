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
  public class HotelNumberRepository : Repository<HotelNumber>, IHotelNumberRepository
  {
    private readonly ApplicationDbContext _db;

    public HotelNumberRepository(ApplicationDbContext db) : base(db)
    {
      _db = db;
    }

    public void Save()
    {
      _db.SaveChanges();
    }

    public void Update(HotelNumber entity)
    {
      _db.Update(entity);
    }
  }
}
