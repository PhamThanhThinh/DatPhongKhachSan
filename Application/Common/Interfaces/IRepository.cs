using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
  public interface IRepository<T> where T : class
  {
    IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, bool traked = false);
    T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool traked = false);
    void Add(T entity);
    bool Any(Expression<Func<T, bool>> filter);
    void Remove(T entity);
  }
}
