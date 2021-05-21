using MedicalRecords.Domain.Entities;
using MedicalRecords.Domain.Requests.Patient;
using MedicalRecords.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedicalRecords.Domain.Mappers
{
    class PatientMapper : IPatientMapper
    {
        private readonly IRiskFactorMapper _riskFactorMapper = new RiskFactorMapper();

        public Patient Map(AddPatientRequest request)
        {
            if (request == null)
            {
                return null;
            }           

            Patient patient = new Patient {
                PatientName = request.PatientName,
                PatientSurname = request.PatientSurname,
                DateOfBirth = request.DateOfBirth,
                PhoneNumber = request.PhoneNumber,
            };

            return patient;

        }

        public Patient Map(EditPatientRequest request)
        {
            if (request == null)
            {
                return null;
            }


            Patient patient = new Patient
            {
                Id = request.Id,
                PatientName = request.PatientName,
                PatientSurname = request.PatientSurname,
                DateOfBirth = request.DateOfBirth,
                PhoneNumber = request.PhoneNumber,
            };

            return patient;
        }

        public PatientResponse Map(Patient request)
        {
            if (request == null)
            {
                return null;
            }

            List<RiskFactorResponse> riskFactors = new List<RiskFactorResponse>();
            if (request.PatientRiskFactors == null)
            {
                riskFactors = null;
            }
            else
            {
                //foreach (PatientRiskFactor p in request.PatientRiskFactors)
                //{
                //    if(p.RiskFactor != null)
                //    {
                //        riskFactors.Add(_riskFactorMapper.Map(p.RiskFactor));
                //    }

                //}

                riskFactors =
                        request.PatientRiskFactors
                            .Select(p => _riskFactorMapper.Map(p.RiskFactor))
                            .ToList();
            }

            PatientResponse response = new PatientResponse
            {
                Id = request.Id,
                PatientName = request.PatientName,
                PatientSurname = request.PatientSurname,
                DateOfBirth = request.DateOfBirth,
                PhoneNumber = request.PhoneNumber,
                RiskFactorResponses = riskFactors
            };

            return response;
        }
    }
}
