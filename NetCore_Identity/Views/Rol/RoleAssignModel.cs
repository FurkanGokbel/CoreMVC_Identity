using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore_Identity.Views.Rol
{
    public class RoleAssignModel
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public bool Exists { get; set; }//ilgili rol kullanicida var mı yok mu ona bakıyoruz
    }
}
