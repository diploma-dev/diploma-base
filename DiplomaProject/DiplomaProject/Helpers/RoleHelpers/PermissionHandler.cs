using Microsoft.AspNetCore.Authorization;

namespace DiplomaProject.Helpers.RoleHelpers
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (requirement.PermissionOperator == PermissionOperator.And)
            {
                foreach (var permission in requirement.Permissions)
                {
                    if (!context.User.HasClaim(PermissionRequirement.ClaimType, permission))
                    {
                        context.Fail();
                        return Task.CompletedTask;
                    }
                }

                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            foreach (var permission in requirement.Permissions)
            {
                if (context.User.HasClaim(PermissionRequirement.ClaimType, permission))
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }

    public enum PermissionOperator
    {
        And = 1, Or = 2
    }

    public class PermissionAuthorizeAttribute : AuthorizeAttribute
    {
        internal const string PolicyPrefix = "PERMISSION_";
        private const string Separator = "_";

        public PermissionAuthorizeAttribute(PermissionOperator permissionOperator, params string[] permissions)
        {
            Policy = $"{PolicyPrefix}{(int)permissionOperator}{Separator}{string.Join(Separator, permissions)}";
        }

        public PermissionAuthorizeAttribute(string permission)
        {
            Policy = $"{PolicyPrefix}{(int)PermissionOperator.And}{Separator}{permission}";
        }

        public static PermissionOperator GetOperatorFromPolicy(string policyName)
        {
            var @operator = int.Parse(policyName.AsSpan(PolicyPrefix.Length, 1));
            return (PermissionOperator)@operator;
        }

        public static string[] GetPermissionsFromPolicy(string policyName)
        {
            return policyName.Substring(PolicyPrefix.Length + 2).Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
