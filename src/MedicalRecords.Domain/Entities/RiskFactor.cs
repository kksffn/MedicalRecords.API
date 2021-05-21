using System;
using System.Collections.Generic;

#nullable disable

namespace MedicalRecords.Domain.Entities
{
    public class RiskFactor
    {
        public int Id { get; set; }
        public string Factor { get; set; }

        /* many to many */
        public List<PatientRiskFactor> PatientRiskFactors { get; set; }
    }
}
