using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalRecords.Domain.Requests.RiskFactor
{
    public class GetRiskFactorRequest
    {
        public int Id { get; set; }
        public string Factor { get; set; }
    }
}
