using MedicalRecords.Domain.Requests.RiskFactor;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalRecords.Domain.Requests.Patient
{
    public class GetPatientRequest
    {
        public int Id { get; set; }
        public List<GetRiskFactorRequest> RiskFactors { get; set; }
    }
}
