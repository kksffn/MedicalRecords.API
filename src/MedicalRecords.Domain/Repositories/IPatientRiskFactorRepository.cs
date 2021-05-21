//using System.Collections.Generic;
//using System.Threading.Tasks;
using MedicalRecords.Domain.Entities;
using System.Threading.Tasks;

namespace MedicalRecords.Domain.Repositories
{
    public interface IPatientRiskFactorRepository : IRepository
    {
        //Task<IEnumerable<PatientRiskFactor>> GetAsync();
        //Task<PatientRiskFactor> GetAsync(int id);
        PatientRiskFactor Add(PatientRiskFactor patientRiskFactor);
        //PatientRiskFactor Update(PatientRiskFactor patientRiskFactor);
        Task<PatientRiskFactor> Delete(PatientRiskFactor patientRiskFactor);
    }
}
