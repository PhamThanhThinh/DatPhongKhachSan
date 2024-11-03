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
  public class AmenityRepository : Repository<Amenity>, IAmenityRepository
  {
    private readonly ApplicationDbContext _db;

    public AmenityRepository(ApplicationDbContext db) : base(db)
    {
      _db = db;
    }

    public void Save()
    {
      _db.SaveChanges();
    }

    public void Update(Amenity entity)
    {
      _db.Amenities.Update(entity);
    }
  }
}
