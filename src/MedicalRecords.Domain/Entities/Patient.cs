using System;
using System.Collections.Generic;

#nullable disable

namespace MedicalRecords.Domain.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsInactive { get; set; }


        /* many to many */
        public List<PatientRiskFactor> PatientRiskFactors { get; set; }
    }
}
