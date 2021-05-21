using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MedicalRecords.Infrastructure.Repositories;
using Shouldly;
using Xunit;
using MedicalRecords.Domain.Entities;
using System;
using System.Linq;

namespace MedicalRecords.Infrastructure.Tests
{
    public class PatientRepositoryTests
    {
        [Fact]
        public async Task should_get_data()
        {
            var options = new DbContextOptionsBuilder<MedicalRecordsContext>()
                .UseInMemoryDatabase(databaseName: "should_get_data")
                .Options;

            await using var context = new TestMedicalRecordsContext(options);
            context.Database.EnsureCreated();

            var sut = new PatientRepository(context);
            var result = await sut.GetAsync();
            result.ShouldNotBeNull();

        }


        [Fact]
        public async Task should_returns_null_with_id_not_present()
        {
            var options = new DbContextOptionsBuilder<MedicalRecordsContext>()
                .UseInMemoryDatabase(databaseName: "should_returns_null_with_id_not_present")
                .Options;

            await using var context = new TestMedicalRecordsContext(options);
            context.Database.EnsureCreated();

            var sut = new PatientRepository(context);
            var result = await sut.GetAsync(6500);
            result.ShouldBeNull();
        }


        //[Theory]
        //[InlineData(50)]
        //public async Task should_return_record_by_id(int id)
        //{
        //    var options = new DbContextOptionsBuilder<MedicalRecordsContext>()
        //        .UseInMemoryDatabase(databaseName: "should_return_record_by_id")
        //        .Options;

        //    await using var context = new TestMedicalRecordsContext(options);
        //    context.Database.EnsureCreated();

        //    var sut = new PatientRepository(context);
        //    var result = await sut.GetAsync(id);
        //    //Assert.True(result.PatientName == "Oleg");
        //    result.Id.ShouldBe(id);
        //}

        [Theory]
        [InlineData("Johana", "Krátká")]
        public async Task should_add_new_Patient(string name, string surname)
        {
            var entity = new Patient();
            entity.PatientName = name;
            entity.PatientSurname = surname;
            DateTime birth = new DateTime(2000, 12, 15);
            entity.DateOfBirth = birth;


            var options = new DbContextOptionsBuilder<MedicalRecordsContext>()
                .UseInMemoryDatabase(databaseName: "should_add_new_Patients")
                .Options;

            await using var context = new TestMedicalRecordsContext(options);
            context.Database.EnsureCreated();

            var sut = new PatientRepository(context);
            sut.Add(entity);

            await sut.UnitOfWork.SaveEntitiesAsync();

            context.Patients
                .FirstOrDefault(x => x.Id == entity.Id)
                .ShouldNotBeNull();
        }

       
    }
}
