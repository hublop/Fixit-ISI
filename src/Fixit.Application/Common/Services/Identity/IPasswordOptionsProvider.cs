using Microsoft.AspNetCore.Identity;

namespace Fixit.Application.Common.Services.Identity
{
    public interface IPasswordOptionsProvider
    {
        PasswordOptions GetPasswordOptions();
    }
}