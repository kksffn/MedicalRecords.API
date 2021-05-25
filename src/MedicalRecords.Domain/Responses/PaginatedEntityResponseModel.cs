using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalRecords.Domain.Responses
{
    public class PaginatedEntityResponseModel<TEntity> where TEntity:class
    {
        public int PageIndex { get; }

        public int PageSize { get; }

        public long Total { get; }

        public IEnumerable<TEntity> Data { get; }


        public PaginatedEntityResponseModel(int pageIndex, int pageSize, 
            long total, IEnumerable<TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Total = total;
            Data = data;
        }
    }
}
