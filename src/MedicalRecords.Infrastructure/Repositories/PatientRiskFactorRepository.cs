using System;
using System.Linq;
using System.Threading.Tasks;
using MedicalRecords.Domain.Entities;
using MedicalRecords.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MedicalRecords.Infrastructure.Repositories
{
    public class PatientRiskFactorRepository : IPatientRiskFactorRepository
    {
        private readonly MedicalRecordsContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public PatientRiskFactorRepository(MedicalRecordsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
                
        public PatientRiskFactor Add(PatientRiskFactor patientRiskFactor)
        {
            return _context.PatientRiskFactors.Add(patientRiskFactor).Entity;
        }

        public async Task<PatientRiskFactor> Delete(PatientRiskFactor patientRiskFactor)
        {
            var pRFToDelete = await _context.PatientRiskFactors
                .Where(pr => ((pr.PatientId == patientRiskFactor.PatientId) && (pr.RiskFactorId == patientRiskFactor.RiskFactorId)))
                .FirstOrDefaultAsync();
            
            
            if(patientRiskFactor != null)
            {
                _context.PatientRiskFactors.Remove(pRFToDelete);
                return pRFToDelete;
            }

            return null;
                      
        }
      
    }
}
