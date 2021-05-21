using System;
using MedicalRecords.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MedicalRecords.Infrastructure.SchemaDefinitions
{
    class PatientEntitySchemaDefinition : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients", MedicalRecordsContext.DEFAULT_SCHEMA);

            builder.HasKey(p => p.Id);

            builder.Property(p => p.PatientName)
                .IsRequired()
                .HasMaxLength(50); 

            builder.Property(p => p.PatientSurname)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(9)
                .IsFixedLength(true); 

            builder.Property(p => p.DateOfBirth)
                .HasColumnType("date");
        }
    }
}
