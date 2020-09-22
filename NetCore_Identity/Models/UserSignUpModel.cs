using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore_Identity.Models
{
    public class UserSignUpModel
    {
        [Display(Name = "Kullanıcı Adı :")]
        [Required(ErrorMessage = "Kullanici Adi bos Gecilemez")]
        public string UserName { get; set; }
        [Display(Name = "Şifre :")]
        [Required(ErrorMessage = "Parola bos Gecilemez")]
        public string Password { get; set; }
        [Display(Name = "Şifre Tekrarı:")]
        [Compare("Password", ErrorMessage = "Parolalar Eşleşmiyor")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Adı :")]
        [Required(ErrorMessage = "Adi bos Gecilemez")]
        public string Name { get; set; }
        [Display(Name = "Soyadı :")]
        [Required(ErrorMessage = "Şifre bos Gecilemez")]
        public string SurName { get; set; }
        [Display(Name = "Email :")]
        [Required(ErrorMessage = "Email bos Gecilemez")]
        public string Email { get; set; }
    }
}
