using MedicalRecords.Domain.Mappers;
using MedicalRecords.Domain.Repositories;
using MedicalRecords.Domain.Requests;
using MedicalRecords.Domain.Requests.Patient;
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

        public PatientService(IPatientRepository patientRepository, IPatientMapper patientMapper)
        {
            _patientRepository = patientRepository;
            _patientMapper = patientMapper;
        }

        public async Task<IEnumerable<PatientResponse>> GetPatientsAsync()
        {
            var result = await _patientRepository.GetAsync();
            return result
                .Select(x => _patientMapper.Map(x));
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

            var result = _patientRepository.Add(patient);

            await _patientRepository.UnitOfWork.SaveChangesAsync();
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
