using MedicalRecords.Domain.Requests;
using MedicalRecords.Domain.Requests.Patient;
using MedicalRecords.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalRecords.Domain.Services
{
    public interface IPatientService
    {
        Task<PaginatedEntityResponseModel<PatientResponse>> GetPatientsAsync(int pageSize, int pageIndex, 
            string orderBy, string order, string search);
        Task<PatientResponse> GetPatientAsync(GetPatientRequest request);
        Task<PatientResponse> AddPatientAsync(AddPatientRequest request);
        Task<PatientResponse> EditPatientAsync(EditPatientRequest request);

        Task<PatientResponse> DeletePatientAsync(DeletePatientRequest request);
        //Task<PatientResponse> GetPatientByRiskFactorIdAsync(int id);

    }
}
