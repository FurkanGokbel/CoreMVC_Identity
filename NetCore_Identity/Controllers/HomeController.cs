using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NetCore_Identity.Context;
using NetCore_Identity.Models;

namespace NetCore_Identity.Controllers
{
    [AutoValidateAntiforgeryToken]//güvenlik önlemi. dışarıdan değer gönderilemez.
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;//usermanager hazır bir yapı onu kullanıyoruz.
        private readonly SignInManager<AppUser> _signInManager;//Buda hazır bir yapı
        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(new UserSignInModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]//güvenlik önlemi. dışarıdan değer gönderilemez.
        public async Task<IActionResult> GirisYap(UserSignInModel model)
        {
            if (ModelState.IsValid)
            {
                var identityResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Index", "Panel");
                }
                ModelState.AddModelError("", "Kullanıcı adı yada şifre hatalı");
            }
            return View("Index", model);
        }
        public IActionResult KayitOl()
        {
            return View(new UserSignUpModel());
        }
        [HttpPost]
        public async Task<IActionResult> KayitOl(UserSignUpModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    Email = model.Email,
                    Name = model.Name,
                    SurName = model.SurName,
                    UserName = model.UserName
                };
                //burada test etmek için ctor oluşturuyoruz.
                var result = await _userManager.CreateAsync(user, model.Password);//passwordu burada vermeliyiz. bizim için şifrelemeyi kendi yapıcak.

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }
        public IActionResult AccessDenied()
        {
            return View(new UserSignInModel());
        }
    }
}
