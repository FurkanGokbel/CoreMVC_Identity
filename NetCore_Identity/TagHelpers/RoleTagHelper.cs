using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NetCore_Identity.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore_Identity.TagHelpers
{
    [HtmlTargetElement("RolGoster")]//tag hekper oluşturduğun zaman viewimport da tanımla
    public class RoleTagHelper : TagHelper
    {
        public int UserId { get; set; }
        private readonly UserManager<AppUser> _userManager;
        public RoleTagHelper(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var user = _userManager.Users.FirstOrDefault(a => a.Id == UserId);
            var roles = await _userManager.GetRolesAsync(user);
            var builder = new StringBuilder();
            foreach (var item in roles)
            {
                builder.Append($"<strong>{item} </strong>");
            }
            output.Content.SetHtmlContent(builder.ToString());
        }
    }
}
