using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
  public class Repository<T> : IRepository<T> where T : class
  {
    private readonly ApplicationDbContext _db;
    internal DbSet<T> dbSet;

    public Repository(ApplicationDbContext db)
    {
      _db = db;
      dbSet = _db.Set<T>();
    }

    public void Add(T entity)
    {
      dbSet.Add(entity);
    }

    public bool Any(Expression<Func<T, bool>> filter)
    {
      return dbSet.Any(filter);
    }

    public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool traked = false)
    {
      IQueryable<T> truyVan;
      if (traked)
      {
        truyVan = dbSet;
      }
      else
      {
        truyVan = dbSet.AsNoTracking();
      }

      if (filter != null)
      {
        truyVan = truyVan.Where(filter);
      }
      
      if (!string.IsNullOrEmpty(includeProperties))
      {
        // dùng được cho Hotel và HotelNumber
        foreach (var property in includeProperties
          .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
          truyVan = truyVan.Include(property.Trim());
        }
      }
      return truyVan.FirstOrDefault();
    }

    public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, bool traked = false)
    {
      IQueryable<T> truyVan;
      if (traked)
      {
        truyVan = dbSet;
      }
      else
      {
        truyVan = dbSet.AsNoTracking();
      }

      if (filter != null)
      {
        truyVan = truyVan.Where(filter);
      }

      if (!string.IsNullOrEmpty(includeProperties))
      {
        // dùng được cho Hotel và HotelNumber
        foreach (var property in includeProperties
          .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
          truyVan = truyVan.Include(property.Trim());
        }
      }
      return truyVan.ToList();
    }

    public void Remove(T entity)
    {
      dbSet.Remove(entity);
    }
  }
}
