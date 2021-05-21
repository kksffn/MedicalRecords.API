using MedicalRecords.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecords.Domain.Repositories
{
    public interface IRiskFactorRepository : IRepository
    {
        Task<IEnumerable<RiskFactor>> GetAsync();
        Task<RiskFactor> GetAsync(int id);
        RiskFactor Add(RiskFactor RiskFactor);
        RiskFactor Update(RiskFactor RiskFactor);
    }
}
