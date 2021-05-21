using MedicalRecords.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace MedicalRecords.Infrastructure.SchemaDefinitions
{
    class PatientRiskFactorEntitySchemaDefinition : IEntityTypeConfiguration<PatientRiskFactor>
    {
        public void Configure(EntityTypeBuilder<PatientRiskFactor> builder)
        {
            builder.ToTable("PatientRiskFactors", MedicalRecordsContext.DEFAULT_SCHEMA);

            builder
                .HasKey(pr => new {pr.PatientId, pr.RiskFactorId } );

            builder
                .HasOne(pr => pr.Patient)
                .WithMany(p => p.PatientRiskFactors)
                .HasForeignKey(pr => pr.PatientId);


            builder
                .HasOne(pr => pr.RiskFactor)
                .WithMany(r => r.PatientRiskFactors)
                .HasForeignKey(pr => pr.RiskFactorId);

        }
    }
}
