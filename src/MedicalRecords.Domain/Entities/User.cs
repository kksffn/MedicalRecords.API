using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalRecords.Domain.Entities
{
    public class User : IdentityUser
    {
        public string UserNickname { get; set; }
    }
}
