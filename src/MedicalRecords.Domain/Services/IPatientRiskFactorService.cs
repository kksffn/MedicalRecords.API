using MedicalRecords.Domain.Requests.PatientRiskFactor;
using MedicalRecords.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecords.Domain.Services
{
    public interface IPatientRiskFactorService
    {
        Task<PatientResponse> AddPatientRiskFactor(AddPatientRiskFactorRequest request);
        Task<PatientRiskFactorResponse> DeletePatientRiskFactor(DeletePatientRiskFactorRequest request);
    }
}
