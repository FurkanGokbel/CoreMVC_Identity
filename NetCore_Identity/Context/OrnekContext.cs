using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore_Identity.Context
{
    public class OrnekContext : IdentityDbContext<AppUser,AppRole,int>//senin userin ve rolün bizim oluşturduğumuz yapı ve key inde int.
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-32DSDLJ\\SQLEXPRESS; database=OrnekIdentity; integrated security=true;");
            base.OnConfiguring(optionsBuilder);
        }
    }

}
