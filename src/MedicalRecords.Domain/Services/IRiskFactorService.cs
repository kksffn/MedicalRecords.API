using MedicalRecords.Domain.Requests.RiskFactor;
using MedicalRecords.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecords.Domain.Services
{
    public interface IRiskFactorService
    {
        Task<IEnumerable<RiskFactorResponse>> GetRiskFactorsAsync();
        Task<RiskFactorResponse> GetRiskFactorAsync(GetRiskFactorRequest request);
        Task<RiskFactorResponse> AddRiskFactorAsync(AddRiskFactorRequest request);
        Task<RiskFactorResponse> EditRiskFactorAsync(EditRiskFactorRequest request);
    }
}
