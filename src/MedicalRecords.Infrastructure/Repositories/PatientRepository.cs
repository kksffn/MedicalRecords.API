using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalRecords.Domain.Entities;
using MedicalRecords.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace MedicalRecords.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly MedicalRecordsContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public PatientRepository(MedicalRecordsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Patient>> GetAsync()
        {
            return await _context.Patients
                .Where(p => !p.IsInactive)
                .AsNoTracking()
                .Include(p => p.PatientRiskFactors)
                .ThenInclude(pr => pr.RiskFactor)
                .ToListAsync();
        }

        public async Task<Patient> GetAsync(int id)
        {
            /* Eager loading*/
            /*LINQ: return result.Where(x => x.Categories.Any(c => c.category == categoryId));
             https://stackoverflow.com/questions/44500007/how-to-query-many-to-many-releationship-in-ef-core
             */
            var patient = await _context.Patients
                .AsNoTracking()
                //.Where(p => p.Id == id)
                .Include(p => p.PatientRiskFactors)
                .ThenInclude(pr => pr.RiskFactor)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (patient == null)
            {
                return null;
            }

            _context.Entry(patient).State = EntityState.Detached;
            return patient;
        }


        public Patient Add(Patient patient)
        {
            return _context.Patients.Add(patient).Entity;
        }

        public Patient Update(Patient patient)
        {
            _context.Entry(patient).State = EntityState.Modified;
            return patient;
        }              

    }
}
