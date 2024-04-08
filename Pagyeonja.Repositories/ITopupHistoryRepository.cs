using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagyeonja.Entities.Entities;
using Pagyeonja.Repositories.Repositories.Models;

namespace Pagyeonja.Repositories.Repositories
{
    public interface ITopupHistoryRepository
    {
       
    Task<IEnumerable<RideHistory>> GetTopupHistories();
    }
}