﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
  public interface IAmenityRepository : IRepository<Amenity>
  {
    void Update(Amenity entity);
    void Save();
  }
}
