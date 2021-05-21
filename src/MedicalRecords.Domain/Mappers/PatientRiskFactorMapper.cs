using MedicalRecords.Domain.Entities;
using MedicalRecords.Domain.Requests.PatientRiskFactor;
using MedicalRecords.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalRecords.Domain.Mappers
{
    public class PatientRiskFactorMapper : IPatientRiskFactorMapper
    {
        public PatientRiskFactor Map(DeletePatientRiskFactorRequest request)
        {
            PatientRiskFactor patientRiskFactor = new PatientRiskFactor
            {
                PatientId = request.PatientId,
                RiskFactorId = request.RiskFactorId
            };

            return patientRiskFactor;
        }
        
        public PatientRiskFactorResponse Map(PatientRiskFactor request)
        {
            PatientRiskFactorResponse patientRiskFactorResponse = new PatientRiskFactorResponse
            {
                PatientId = request.PatientId,
                RiskFactorId = request.RiskFactorId
            };

            return patientRiskFactorResponse;
        }
    }
}
