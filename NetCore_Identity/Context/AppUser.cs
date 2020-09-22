using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore_Identity.Context
{
    public class AppUser:IdentityUser<int>//hazır tablonun üstüne bunlarıda ekledik
    {
        public string PictureUrl { get; set; }
        //3NF ye aykırı bir işlem. Ayrı Tablo olarak tutman gerekirdi.
        public string Gender { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
    }
}
