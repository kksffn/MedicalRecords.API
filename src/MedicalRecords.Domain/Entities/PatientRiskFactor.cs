using System;
using System.Collections.Generic;

#nullable disable

namespace MedicalRecords.Domain.Entities
{
    public class PatientRiskFactor
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int RiskFactorId { get; set; }
        public RiskFactor RiskFactor { get; set; }
    }
}
