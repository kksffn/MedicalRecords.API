using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalRecords.Domain.Requests.PatientRiskFactor
{
    public class AddPatientRiskFactorRequest
    {
        public int PatientId { get; set; }
        public int RiskFactorId { get; set; }
    }
}
