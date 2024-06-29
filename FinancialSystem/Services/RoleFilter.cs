using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using FinancialSystem.Models.DB.DBModels;
using FinancialSystem.Models.UserModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinancialSystem.Services
{
    public class RoleFilter:IAuthorizationFilter
    {
        private readonly AppDbContext _context;

        public RoleFilter(AppDbContext context)
        {
            _context = context;
        }
        public void OnAuthorization(AuthorizationFilterContext context){
            var claim = context.HttpContext.User;
            if (!claim.Identity.IsAuthenticated){
                context.Result = new UnauthorizedResult();
                return;
            }

            var user = JsonSerializer.Deserialize<UserRet>(claim.FindFirst("user")?.Value);

            if (user == null || !user.Roles.Any(role => role.Name == "admin")){
                context.Result = new ForbidResult();
            }
        }
    }
}