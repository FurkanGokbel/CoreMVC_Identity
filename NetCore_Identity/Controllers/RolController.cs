using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCore_Identity.Context;
using NetCore_Identity.Models;
using NetCore_Identity.Views.Rol;

namespace NetCore_Identity.Controllers
{
    [Authorize]//burda işlem yapılacaksa kullanıcı giriş yapmış olsun
    public class RolController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RolController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }
        public IActionResult AddRole()
        {
            return View(new RoleModel());
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleModel model)
        {
            if (ModelState.IsValid)//tanımlanan requiredlerden geçmişşe
            {
                AppRole role = new AppRole
                {
                    Name = model.Name
                };
                var identityResult = await _roleManager.CreateAsync(role);
                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                //Bu hataların gözükmesi için Indexte div eklemeyi unutma
            }
            return View(model);
        }
        public IActionResult UpdateRole(int id)
        {
            var role = _roleManager.Roles.FirstOrDefault(I => I.Id == id);
            //rol boş mu dolu kontrol edilmesi lazımdı.
            RoleUpdateModel model = new RoleUpdateModel
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRole(RoleUpdateModel model)
        {
            var tobeUpdateRole = _roleManager.Roles.
                 Where(I => I.Id == model.Id).FirstOrDefault();
            tobeUpdateRole.Name = model.Name;
            var identityResult = await _roleManager.UpdateAsync(tobeUpdateRole);
            if (identityResult.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);

        }
        public async Task<IActionResult> DeleteRole(int id)
        {
            var tobeDeletedRole = _roleManager.Roles.
                FirstOrDefault(I => I.Id == id);
            var identityResult = await _roleManager.DeleteAsync(tobeDeletedRole);
            if (identityResult.Succeeded)
            {
                return RedirectToAction("Index");
            }
            TempData["Errors"] = identityResult.Errors;

            return RedirectToAction("Index");

        }
        public IActionResult UserList()
        {
            return View(_userManager.Users.ToList());
        }
        public async Task<IActionResult> AssignRole(int id)
        {
            var user = _userManager.Users.FirstOrDefault(I => I.Id == id);
            TempData["UserId"] = user.Id;
            var roles = _roleManager.Roles.ToList();
            var userRoles = await _userManager.GetRolesAsync(user);
            List<RoleAssignModel> models = new List<RoleAssignModel>();
            foreach (var item in roles)
            {
                RoleAssignModel model = new RoleAssignModel();
                model.RoleId = item.Id;
                model.Name = item.Name;
                model.Exists = userRoles.Contains(item.Name);
                models.Add(model);
            }
            return View(models);
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(List<RoleAssignModel> models)
        {
            var userId = (int)TempData["UserId"];
            var user = _userManager.Users.FirstOrDefault(I => I.Id == userId);

            foreach (var item in models)
            {
                if (item.Exists)//exits clicklenmiş mi?
                {
                    await _userManager.AddToRoleAsync(user, item.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.Name);

                }
            }
            return RedirectToAction("UserList");
        }

    }
}
