using MedicalRecords.Domain.Entities;
using MedicalRecords.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalRecords.Infrastructure.Repositories
{
    public class RiskFactorRepository : IRiskFactorRepository
    {
        private readonly MedicalRecordsContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public RiskFactorRepository(MedicalRecordsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<RiskFactor>> GetAsync()
        {
            return await _context.RiskFactors
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<RiskFactor> GetAsync(int id)
        {
            var factor = await _context.RiskFactors
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (factor == null)
            {
                return null;
            }

            _context.Entry(factor).State = EntityState.Detached;
            return factor;           
        }


        public RiskFactor Add(RiskFactor riskFactor)
        {
            return _context.RiskFactors.Add(riskFactor).Entity;
        }

        public RiskFactor Update(RiskFactor riskFactor)
        {
            _context.Entry(riskFactor).State = EntityState.Modified;
            return riskFactor;
        }
    }
}
