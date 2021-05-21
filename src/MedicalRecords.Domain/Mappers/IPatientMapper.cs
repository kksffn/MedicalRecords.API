using MedicalRecords.Domain.Entities;
using MedicalRecords.Domain.Requests.Patient;
using MedicalRecords.Domain.Responses;

namespace MedicalRecords.Domain.Mappers
{
    interface IPatientMapper
    {
        Patient Map(AddPatientRequest request);
        Patient Map(EditPatientRequest request);
        PatientResponse Map(Patient request);
    }
}
