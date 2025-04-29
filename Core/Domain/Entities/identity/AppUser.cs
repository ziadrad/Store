using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Domain.Entities.identity
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }

public Address Address { get; set; }
    }
}
