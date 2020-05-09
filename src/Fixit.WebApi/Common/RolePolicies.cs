namespace Fixit.WebApi.Common
{
    public static class RolePolicies
    {
        public const string RequireAuthorizedUser = "AuthUser";
        public const string RequireAdmin = "Admin";
        public const string RequireCustomer = "Customer";
        public const string RequireContractor = "Contractor";
    }
}