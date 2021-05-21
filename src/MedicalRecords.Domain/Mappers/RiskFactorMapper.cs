using MedicalRecords.Domain.Entities;
using MedicalRecords.Domain.Requests.RiskFactor;
using MedicalRecords.Domain.Responses;

namespace MedicalRecords.Domain.Mappers
{
    class RiskFactorMapper : IRiskFactorMapper
    {
        public RiskFactor Map(AddRiskFactorRequest request)
        {
            if (request == null)
            {
                return null;
            }

            RiskFactor factor = new RiskFactor
            {               
                Factor = request.Factor,
            };

            return factor;

        }
        public RiskFactor Map(EditRiskFactorRequest request)
        {
            if (request == null)
            {
                return null;
            }

            RiskFactor factor = new RiskFactor
            {
                Id = request.Id,
                Factor = request.Factor,
            };

            return factor;
        }
        public RiskFactorResponse Map(RiskFactor riskFactor)
        {
            if (riskFactor == null)
            {
                return null;
            }

            return new RiskFactorResponse
            {
                Id = riskFactor.Id,
                Factor = riskFactor.Factor
            };
        }
    }
}
