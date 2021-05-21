using MedicalRecords.Domain.Requests.RiskFactor;
using System;
using System.Collections.Generic;

namespace MedicalRecords.Domain.Requests.Patient
{
    public class AddPatientRequest
    {
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<GetRiskFactorRequest> RiskFactors { get; set; }
    }
}
