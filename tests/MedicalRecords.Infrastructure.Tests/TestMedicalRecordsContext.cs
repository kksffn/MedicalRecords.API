using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecords.Infrastructure.Tests
{
    public class TestMedicalRecordsContext : MedicalRecordsContext
    {
        public TestMedicalRecordsContext(DbContextOptions<MedicalRecordsContext> options) : base(options)
        {
        }
    }
}
