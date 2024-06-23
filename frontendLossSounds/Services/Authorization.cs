using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;
using frontendLossSounds.Models;


namespace frontendLossSounds.Services
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public string Role { get; }

        public RoleRequirement(string role)
        {
            Role = role;
        }
    }

    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute(string role) : base(typeof(RoleAuthorizeFilter))
        {
            Arguments = new object[] { new RoleRequirement(role) };
        }
    }

    public class RoleAuthorizeFilter : IAuthorizationFilter
    {
        private readonly RoleRequirement _requiredRole;

        public RoleAuthorizeFilter(RoleRequirement requiredRole)
        {
            _requiredRole = requiredRole;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var session = context.HttpContext.Session;
            var userLoggedString = session.GetString("UserLogged");

            if (string.IsNullOrEmpty(userLoggedString))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Index"
                }));
                return;
            }

            var settings = JsonSerializer.Deserialize<Settings>(userLoggedString);

            if (settings == null || !IsRoleAuthorized(settings.RolID, _requiredRole.Role))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Unathorized"
                }));
                return;
            }
        }

        private bool IsRoleAuthorized(int userRolID, string requiredRole)
        {
            switch (userRolID)
            {
                case 1: // IT_ADMIN
                    return requiredRole == Role.IT_ADMIN;
                case 2: // ADMIN
                    return requiredRole == Role.ADMIN;
                case 3: // USER
                    return requiredRole == Role.USER;
                default:
                    return false;
            }
        }
    }
}