using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Pagyeonja.Entities.Entities;

namespace Pagyeonja.Repositories.Repositories
{
    public class SuspensionRepository : ISuspensionRepository
    {
        private readonly HitchContext _context;
        public SuspensionRepository(HitchContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Suspension>> GetSuspensions()
        {
            return await _context.Suspensions.OrderByDescending(a => a.SuspensionId).ToListAsync();
        }

        public async Task<Suspension> GetSuspension(Guid userid, string usertype)
        {
            return await _context.Suspensions
                .Where(s => s.UserId == userid && s.UserType == usertype && s.SuspensionDate >= DateTime.Now && s.Status == true)
                .OrderBy(s => s.InvokedSuspensionDate).FirstOrDefaultAsync();
        }

        public async Task<Suspension> UpdateSuspension(Suspension suspension)
        {
            _context.Entry(suspension).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return suspension;
        }
    }
}