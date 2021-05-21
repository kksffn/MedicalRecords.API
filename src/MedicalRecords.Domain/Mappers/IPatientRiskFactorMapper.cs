using MedicalRecords.Domain.Entities;
using MedicalRecords.Domain.Requests.PatientRiskFactor;
using MedicalRecords.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalRecords.Domain.Mappers
{
    public interface IPatientRiskFactorMapper
    {
        PatientRiskFactor Map(DeletePatientRiskFactorRequest request);
        PatientRiskFactorResponse Map(PatientRiskFactor request);
    }
}
