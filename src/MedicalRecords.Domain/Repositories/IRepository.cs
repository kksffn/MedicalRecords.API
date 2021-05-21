using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalRecords.Domain.Repositories
{
    /*Všichni budou využívat IUnitOfWork!*/
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
