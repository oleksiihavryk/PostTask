using Microsoft.AspNetCore.Identity;

namespace PostTask.Authentication.Domain;
/// <summary>
///     Identity role of user
/// </summary>
public class Role : IdentityRole
{
    public Role()
        : base()
    {
    }
    public Role(Roles role)
        : base(role.ToString())
    {
    }
}