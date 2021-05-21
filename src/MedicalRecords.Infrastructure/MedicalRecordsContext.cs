//using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MedicalRecords.Domain.Entities;
using MedicalRecords.Domain.Repositories;
using MedicalRecords.Infrastructure.SchemaDefinitions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MedicalRecords.Infrastructure
{
    public class MedicalRecordsContext : IdentityDbContext<User>, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "MedicalRecords";
        public DbSet<Patient> Patients { get; set; }
        public DbSet<RiskFactor> RiskFactors { get; set; }
        public DbSet<PatientRiskFactor> PatientRiskFactors { get; set; }


        public MedicalRecordsContext(DbContextOptions<MedicalRecordsContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Od verze 2.2 prý funguje následuící a nemusí se přidávat každá konfigurace ručně; výhodné pro větší appky:
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyConfiguration(new PatientEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new RiskFactorEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new PatientRiskFactorEntitySchemaDefinition());

            //Because IdentityDbContext extends DbContext: 
            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await SaveChangesAsync(cancellationToken);
            return true;
        }

    }
}
