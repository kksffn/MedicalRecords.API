using System;
using MedicalRecords.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MedicalRecords.Infrastructure.SchemaDefinitions
{
    class RiskFactorEntitySchemaDefinition : IEntityTypeConfiguration<RiskFactor>
    {
        public void Configure(EntityTypeBuilder<RiskFactor> builder)
        {
            builder.ToTable("RiskFactors", MedicalRecordsContext.DEFAULT_SCHEMA);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Factor)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}