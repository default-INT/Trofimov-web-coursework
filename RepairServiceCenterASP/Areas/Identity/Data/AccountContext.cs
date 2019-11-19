using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RepairServiceCenterASP.Models
{
    public class AccountContext : IdentityDbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options)
            : base(options)
        {
        }

    }
}
