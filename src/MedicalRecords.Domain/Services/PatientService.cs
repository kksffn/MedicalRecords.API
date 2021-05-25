using MedicalRecords.Domain.Entities;
using MedicalRecords.Domain.Mappers;
using MedicalRecords.Domain.Repositories;
using MedicalRecords.Domain.Requests;
using MedicalRecords.Domain.Requests.Patient;
using MedicalRecords.Domain.Requests.PatientRiskFactor;
using MedicalRecords.Domain.Requests.RiskFactor;
using MedicalRecords.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalRecords.Domain.Services
{
    class PatientService : IPatientService
    {

        private readonly IPatientRepository _patientRepository;
        private readonly IPatientMapper _patientMapper;
        private readonly IPatientRiskFactorService _patientRiskFactorService;

        public PatientService(IPatientRepository patientRepository, IPatientMapper patientMapper, 
            IPatientRiskFactorService patientRiskFactorService)
        {
            _patientRepository = patientRepository;
            _patientMapper = patientMapper;
            _patientRiskFactorService = patientRiskFactorService;
        }

        public async Task<PaginatedEntityResponseModel<PatientResponse>> GetPatientsAsync(int pageSize, int pageIndex,
            string orderBy, string order, string search)
        {
            IEnumerable<Patient> patientsFromDb = await _patientRepository.GetAsync(pageSize, pageIndex,
            orderBy, order, search);

            int totalPatients = await _patientRepository.CountPatients(search); 

            IEnumerable<PatientResponse> patientResponses = patientsFromDb
                .Select(x => _patientMapper.Map(x));

            return new PaginatedEntityResponseModel<PatientResponse>(
                pageIndex, pageSize, totalPatients, patientResponses);
        }

        public async Task<PatientResponse> GetPatientAsync(GetPatientRequest request)
        {
            if (request?.Id == null)
            {
                throw new ArgumentNullException();
            }

            var entity = await _patientRepository.GetAsync(request.Id);
            return _patientMapper.Map(entity);
        }

        public async Task<PatientResponse> AddPatientAsync(AddPatientRequest request)
        {
            var patient = _patientMapper.Map(request);

            Patient result = _patientRepository.Add(patient);

            await _patientRepository.UnitOfWork.SaveChangesAsync();


            // Create new records in joining table:
            if (request.RiskFactors != null && result != null)
            {
                foreach (GetRiskFactorRequest riskFactor in request.RiskFactors)
                {
                    AddPatientRiskFactorRequest addPRFRequest = new AddPatientRiskFactorRequest
                    {
                        PatientId = result.Id,
                        RiskFactorId = riskFactor.Id
                    };
                    await _patientRiskFactorService.AddPatientRiskFactor(addPRFRequest);
                }
            }
            return _patientMapper.Map(result);
        }

        public async Task<PatientResponse> EditPatientAsync(EditPatientRequest request)
        {
            var existingRecord = await _patientRepository.GetAsync(request.Id);

            if (existingRecord == null)
            {
                throw new ArgumentException($"Entity with {request.Id} does not exist!");
            }

            var entity = _patientMapper.Map(request);
            var result = _patientRepository.Update(entity);

            // Delete old records in joining table for existing record:
            if (existingRecord.PatientRiskFactors != null)
            {
                foreach (PatientRiskFactor patientRiskFactor in existingRecord.PatientRiskFactors)
                {
                    DeletePatientRiskFactorRequest deletePRF = new DeletePatientRiskFactorRequest
                    {
                        PatientId = patientRiskFactor.PatientId,
                        RiskFactorId = patientRiskFactor.RiskFactorId
                    };
                    await _patientRiskFactorService.DeletePatientRiskFactor(deletePRF);
                }
            }
            

            // Create new records in joining table:
            if (request.RiskFactors != null)
            {
                foreach (GetRiskFactorRequest riskFactor in request.RiskFactors)
                {
                    AddPatientRiskFactorRequest addPRFRequest = new AddPatientRiskFactorRequest
                    {
                        PatientId = request.Id,
                        RiskFactorId = riskFactor.Id
                    };
                    await _patientRiskFactorService.AddPatientRiskFactor(addPRFRequest);
                }
            }            

            // Confirm changes:
            await _patientRepository.UnitOfWork.SaveChangesAsync();
            return _patientMapper.Map(result);
        }

        public async Task<PatientResponse> DeletePatientAsync(DeletePatientRequest request)
        {
            if(request?.Id == null)
            {
                throw new ArgumentNullException();
            }

            var result = await _patientRepository.GetAsync(request.Id);
            result.IsInactive = true;

            _patientRepository.Update(result);
            await _patientRepository.UnitOfWork.SaveChangesAsync();

            return _patientMapper.Map(result);
        }
        
    }
}
