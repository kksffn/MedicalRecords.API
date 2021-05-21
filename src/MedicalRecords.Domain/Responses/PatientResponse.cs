using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalRecords.Domain.Responses
{
    public class PatientResponse
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
       
        public List<RiskFactorResponse> RiskFactorResponses { get; set; }

    }
}
