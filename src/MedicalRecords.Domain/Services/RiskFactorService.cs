using MedicalRecords.Domain.Entities;
using MedicalRecords.Domain.Mappers;
using MedicalRecords.Domain.Repositories;
using MedicalRecords.Domain.Requests.RiskFactor;
using MedicalRecords.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecords.Domain.Services
{
    class RiskFactorService : IRiskFactorService
    {
        private readonly IRiskFactorRepository _riskFactorRepository;
        private readonly IRiskFactorMapper _riskFactorMapper;

        private readonly IPatientRepository _patientRepository;
        private readonly IPatientMapper _patientMapper;

        public RiskFactorService(IRiskFactorRepository riskFactorRepository, IRiskFactorMapper riskFactorMapper, 
            IPatientRepository patientRepository, IPatientMapper patientMapper)
        {
            _riskFactorRepository = riskFactorRepository;
            _riskFactorMapper = riskFactorMapper;
            _patientRepository = patientRepository;
            _patientMapper = patientMapper;
        }

        public async Task<PaginatedEntityResponseModel<RiskFactorResponse>> GetRiskFactorsAsync(int pageSize, int pageIndex)
        {
            IEnumerable<RiskFactor> riskFactorsFromDb = 
                await _riskFactorRepository.GetAsync(pageSize, pageIndex);

            IEnumerable<RiskFactorResponse> riskFactorResponses = riskFactorsFromDb
                .Select(x => _riskFactorMapper.Map(x));

            int totalRiskFactors = await _riskFactorRepository.CountRiskFactors();

            return new PaginatedEntityResponseModel<RiskFactorResponse>
                (pageIndex, pageSize, totalRiskFactors, riskFactorResponses);
        }

        public async Task<RiskFactorResponse> GetRiskFactorAsync(GetRiskFactorRequest request)
        {
            if (request?.Id == null)
            {
                throw new ArgumentNullException();
            }

            var entity = await _riskFactorRepository.GetAsync(request.Id);
            return entity == null ? null :_riskFactorMapper.Map(entity);
        }        

        //TODO
        public async Task<IEnumerable<PatientResponse>> GetPatientsByRiskFactorAsync(GetRiskFactorRequest request)
        {
            if (request?.Id == null) throw new ArgumentNullException();
            //var result = await _patientRepository.GetPatientByRiskFactorIdAsync(request.Id);
            return null; //result.Select(_patientMapper.Map);
        }
        
        public async Task<RiskFactorResponse> AddRiskFactorAsync(AddRiskFactorRequest request)
        {
            var riskFactor = new RiskFactor();
            riskFactor.Factor = request.Factor;
            var result = _riskFactorRepository.Add(riskFactor);

            await _riskFactorRepository.UnitOfWork.SaveChangesAsync();
            return _riskFactorMapper.Map(result);
        }

        public async Task<RiskFactorResponse> EditRiskFactorAsync(EditRiskFactorRequest request)
        {
            var existingRecord = await _riskFactorRepository.GetAsync(request.Id);

            if (existingRecord == null)
            {
                throw new ArgumentException($"Entity with {request.Id} does not exist!");
            }

            var entity = _riskFactorMapper.Map(request);
            var result = _riskFactorRepository.Update(entity);

            await _riskFactorRepository.UnitOfWork.SaveChangesAsync();
            return _riskFactorMapper.Map(result);
        }


    }
}
