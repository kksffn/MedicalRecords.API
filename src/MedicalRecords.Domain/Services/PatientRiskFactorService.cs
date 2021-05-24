using MedicalRecords.Domain.Entities;
using MedicalRecords.Domain.Mappers;
using MedicalRecords.Domain.Repositories;
using MedicalRecords.Domain.Requests.PatientRiskFactor;
using MedicalRecords.Domain.Responses;
using System;
using System.Threading.Tasks;

namespace MedicalRecords.Domain.Services
{
    class PatientRiskFactorService : IPatientRiskFactorService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IRiskFactorRepository _riskFactorRepository;
        private readonly IPatientRiskFactorRepository _patientRiskFactorRepository;
        private readonly IPatientMapper _patientMapper;
        private readonly IPatientRiskFactorMapper _patientRiskFactorMapper;

        public PatientRiskFactorService(IPatientRepository patientRepository,
            IRiskFactorRepository riskFactorRepository,
            IPatientRiskFactorRepository patientRiskFactorRepository,
            IPatientMapper patientMapper,
            IPatientRiskFactorMapper patientRiskFactorMapper)
        {
            _patientRepository = patientRepository;
            _riskFactorRepository = riskFactorRepository;
            _patientRiskFactorRepository = patientRiskFactorRepository;
            _patientMapper = patientMapper;
            _patientRiskFactorMapper = patientRiskFactorMapper;
        }


        public async Task<PatientResponse> AddPatientRiskFactor(AddPatientRiskFactorRequest request)
        {

            if ( (request?.PatientId == null) || (request?.RiskFactorId == null) )
            {
                throw new ArgumentNullException();
            }

            Patient patient = await _patientRepository.GetAsync(request.PatientId);            
            RiskFactor riskFactor = await _riskFactorRepository.GetAsync(request.RiskFactorId);            

            PatientRiskFactor patientRiskFactor = new PatientRiskFactor
            {
                //Přes Id to funguje, přes celé entity ne!!!
                PatientId = patient.Id,
                RiskFactorId = riskFactor.Id
            };
            
            _patientRiskFactorRepository.Add(patientRiskFactor);

            await _patientRiskFactorRepository.UnitOfWork.SaveChangesAsync();
            
            //aktualizace pacienta
            patient = await _patientRepository.GetAsync(request.PatientId);
            return _patientMapper.Map(patient);
        }
        public async Task<PatientRiskFactorResponse> DeletePatientRiskFactor(DeletePatientRiskFactorRequest request)
        {
            if ((request?.PatientId == null) || (request?.RiskFactorId == null))
            {
                throw new ArgumentNullException();
            }

            PatientRiskFactor patientRiskFactor = _patientRiskFactorMapper.Map(request);

            await _patientRiskFactorRepository.Delete(patientRiskFactor);
            await _patientRiskFactorRepository.UnitOfWork.SaveChangesAsync();

            return _patientRiskFactorMapper.Map(patientRiskFactor);

        }
    }
}
