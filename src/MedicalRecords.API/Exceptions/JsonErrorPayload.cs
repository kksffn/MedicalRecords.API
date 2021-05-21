using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalRecords.API.Exceptions
{
    public class JsonErrorPayload
    {
        public int EventId { get; set; }
        public Object DetailedMessage { get; set; }
    }
}
