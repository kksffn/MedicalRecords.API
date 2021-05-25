using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalRecords.Domain.Entities;
using MedicalRecords.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace MedicalRecords.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly MedicalRecordsContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public PatientRepository(MedicalRecordsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Patient>> GetAsync(int pageSize, int pageIndex,
            string orderBy, string order, string search)
        {
            // if search string is set, then search in patientName or patientSurname
            var searchedPatients = SearchPatients(search);

            // if orderBy is set then order the patient by patientName, patientSurname or DateOfBirth (Id is default) 
            var orderedPatients = OrderPatients(searchedPatients, orderBy, order);

            var selectedPatients = await orderedPatients
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();            

            return selectedPatients;
        }
        public async Task<Patient> GetAsync(int id)
        {
            /* Eager loading*/
            /*LINQ: return result.Where(x => x.Categories.Any(c => c.category == categoryId));
             https://stackoverflow.com/questions/44500007/how-to-query-many-to-many-releationship-in-ef-core
             */
            var patient = await _context.Patients
                .AsNoTracking()
                //.Where(p => p.Id == id)
                .Include(p => p.PatientRiskFactors)
                .ThenInclude(pr => pr.RiskFactor)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (patient == null)
            {
                return null;
            }

            _context.Entry(patient).State = EntityState.Detached;
            return patient;
        }

        public Patient Add(Patient patient)
        {
            return _context.Patients.Add(patient).Entity;
        }

        public Patient Update(Patient patient)
        {
            _context.Entry(patient).State = EntityState.Modified;
            return patient;
        }              

        //HelperMethods
        private IQueryable<Patient> SearchPatients(string search)
        {
            if (search == "")
            {
                return _context.Patients
                .Where(p => !p.IsInactive)
                .AsNoTracking()
                .Include(p => p.PatientRiskFactors)
                .ThenInclude(pr => pr.RiskFactor);
            }

            return _context.Patients
                .Where(p => !p.IsInactive)
                .Where(p => EF.Functions.Like(p.PatientSurname, '%' + search + '%') 
                | EF.Functions.Like(p.PatientName, '%' + search + '%'))
                .AsNoTracking()
                .Include(p => p.PatientRiskFactors)
                .ThenInclude(pr => pr.RiskFactor);
        }

        private IQueryable<Patient> OrderPatients(IQueryable<Patient> searchedPatients, string orderBy, string order)
        {
            IQueryable<Patient> orderedPatients;

            switch (orderBy.ToLower())
            {
                case "patientsurname":
                    {
                        orderedPatients = searchedPatients
                            .OrderBy(p => p.PatientSurname)
                            .ThenBy(p => p.PatientName);
                        break;
                    }

                case "patientname":
                    {
                        orderedPatients = searchedPatients
                            .OrderBy(p => p.PatientName)
                            .ThenBy(p => p.PatientSurname);
                        break;
                    }

                case "dateofbirth":
                    {
                        orderedPatients = searchedPatients
                            .OrderBy(p => p.DateOfBirth);
                        break;
                    }

                default:
                    {
                        orderedPatients = searchedPatients
                            .OrderBy(p => p.Id);
                        break;
                    }
            }

            if(order.ToLower() == "desc")
            {
                return orderedPatients.Reverse();
            }

            return orderedPatients;

        }

        public async Task<int> CountPatients(string search)
        {
            return await _context.Patients
                .Where(p => !p.IsInactive)
                .Where(p => EF.Functions.Like(p.PatientSurname, '%' + search + '%')
                | EF.Functions.Like(p.PatientName, '%' + search + '%'))
                .CountAsync();
        }
    }
}
