using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
  public class HotelRepository : Repository<Hotel>, IHotelRepository
  {
    private readonly ApplicationDbContext _db;

    public HotelRepository(ApplicationDbContext db) : base(db)
    {
      _db = db;
    }

    //public void Add(Hotel entity)
    //{
    //  _db.Add(entity);
    //}

    //public Hotel Get(Expression<Func<Hotel, bool>> filter, string? includeProperties = null)
    //{
    //  IQueryable<Hotel> truyVan = _db.Set<Hotel>();
    //  if (filter != null)
    //  {
    //    truyVan = truyVan.Where(filter);
    //  }
    //  if (!string.IsNullOrEmpty(includeProperties))
    //  {
    //    // dùng được cho Hotel và HotelNumber
    //    foreach (var property in includeProperties
    //      .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
    //    {
    //      truyVan = truyVan.Include(property);
    //    }
    //  }
    //  return truyVan.FirstOrDefault();
    //}

    //public IEnumerable<Hotel> GetAll(Expression<Func<Hotel, bool>>? filter = null, string? includeProperties = null)
    //{
    //  IQueryable<Hotel> truyVan = _db.Set<Hotel>();
    //  if (filter != null)
    //  {
    //    truyVan = truyVan.Where(filter);
    //  }
    //  if (!string.IsNullOrEmpty(includeProperties))
    //  {
    //    // dùng được cho Hotel và HotelNumber
    //    foreach (var property in includeProperties
    //      .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
    //    {
    //      truyVan = truyVan.Include(property);
    //    }
    //  }
    //  return truyVan.ToList();
    //}

    //public IEnumerable<Hotel> GetAll(Expression<Func<Hotel, bool>>? filter = null, string? includeProperties = null)
    //{
    //  IQueryable<Hotel> truyVan = _db.Set<Hotel>();

    //  if (filter != null)
    //  {
    //    truyVan = truyVan.Where(filter);
    //  }

    //  if (!string.IsNullOrEmpty(includeProperties))
    //  {
    //    foreach (var property in includeProperties
    //        .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
    //    {
    //      truyVan = truyVan.Include(property);
    //    }
    //  }

    //  return truyVan.ToList(); // Trả về danh sách.
    //}

    //public void Remove(Hotel entity)
    //{
    //  _db.Remove(entity);
    //}

    public void Save()
    {
      _db.SaveChanges();
    }

    public void Update(Hotel entity)
    {
      _db.Hotels.Update(entity);
    }
  }
}
