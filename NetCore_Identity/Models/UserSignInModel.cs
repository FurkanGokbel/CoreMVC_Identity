using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore_Identity.Models
{
    public class UserSignInModel
    {
        [Display(Name ="Kullanıcı Adı :")]
        [Required(ErrorMessage ="Kullanici Adi bos Gecilemez")]
        public string UserName { get; set; }
        [Display(Name ="Şifre :")]
        [Required(ErrorMessage = "Sifre bos Gecilemez")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
