using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MedicalRecords.Domain.Entities;



namespace MedicalRecords.Domain.Repositories
{
    /*Definuje operace pro čtení a zápis nad kolekcemi dat, UnitOFWork se postará o uložení změn v DB*/
    public interface IPatientRepository : IRepository
    {
        Task<IEnumerable<Patient>> GetAsync();
        Task<Patient> GetAsync(int id);
        Patient Add(Patient patient);
        Patient Update(Patient patient);
        
        //Task<IEnumerable<Patient>> GetPatientByRiskFactorIdAsync(int id);
    }
}
