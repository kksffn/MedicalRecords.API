using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalRecords.Domain.Requests.RiskFactor
{
    public class EditRiskFactorRequest
    {
        public int Id { get; set; }
        public string Factor { get; set; }
    }
}
