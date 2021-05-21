using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalRecords.Domain.Responses
{
    public class PatientRiskFactorResponse
    {
        public int PatientId { get; set; }
        public int RiskFactorId { get; set; }
    }
}
