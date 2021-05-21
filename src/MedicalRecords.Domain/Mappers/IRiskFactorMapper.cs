using MedicalRecords.Domain.Entities;
using MedicalRecords.Domain.Requests.RiskFactor;
using MedicalRecords.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalRecords.Domain.Mappers
{
    interface IRiskFactorMapper
    {
        public RiskFactor Map(AddRiskFactorRequest request);
        public RiskFactor Map(EditRiskFactorRequest request);
        RiskFactorResponse Map(RiskFactor riskFactor);
    }
}
