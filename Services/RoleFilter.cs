using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using FinancialSystem.Models.UserModels;
using Google.Rpc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinancialSystem.Services
{
    public class RoleFilter:IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context){
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated){
                context.Result = new UnauthorizedResult();
                return;
            } 
            var claim = JsonSerializer.Deserialize<User>(user.FindFirst("usuario")?.Value);
            if (claim == null || claim.role != "admin"){
                context.Result = new ForbidResult();
            }
        }
    }
}