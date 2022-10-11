using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PostTask.Authentication.Core.ClaimProvider;
using PostTask.Authentication.Domain;
using Init = PostTask.Authentication.Core.DatabaseInitializer.IIdentityDatabaseInitializer;

namespace PostTask.Authentication.Core.DatabaseInitializer;
/// <summary>
///     Implementation of identity database initializer for common database
/// </summary>
public class CommonIdentityDatabaseInitializer : IIdentityDatabaseInitializer
{
    /// <summary>
    ///     Role manager provider
    /// </summary>
    private readonly RoleManager<Role> _roleManager;
    /// <summary>
    ///     User manager provider
    /// </summary>
    private readonly UserManager<User> _userManager;
    /// <summary>
    ///     Claim provider service
    /// </summary>
    private readonly IClaimProvider<User> _claimProvider;
    /// <summary>
    ///     Provided users into application
    /// </summary>
    private readonly IEnumerable<UserData> _users;
    /// <summary>
    ///     Provided admins into application 
    /// </summary>
    private readonly IEnumerable<UserData> _admins;

    public CommonIdentityDatabaseInitializer(
        RoleManager<Role> roleManager, 
        UserManager<User> userManager,
        IClaimProvider<User> claimProvider,
        IOptions<IdentityDatabaseInitializerOptions> options)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _claimProvider = claimProvider;

        _admins = options.Value.Admins;
        _users = options.Value.Users;
    }

    /// <summary>
    ///     Implementation of database initialization by chosen mode
    /// </summary>
    /// <param name="mode">
    ///     Database initialization mode
    /// </param>
    public async Task Initialize(IIdentityDatabaseInitializer.InitializationMode mode)
    {
        if (mode.HasFlag(Init.InitializationMode.Roles))
        {
            var roles = Enum.GetValues<Roles>();
            foreach (var r in roles)
                if ((await _roleManager.FindByNameAsync(r.ToString())) == null)
                    await _roleManager.CreateAsync(new Role(role: r));
        }

        if (mode.HasFlag(Init.InitializationMode.Admins))
        {
            foreach (var ud in _admins)
            {
                if ((await _userManager.FindByNameAsync(ud.Username)) == null)
                {
                    await ProvideUserWithRole(
                        user: new User()
                        {
                            UserName = ud.Username
                        },
                        pass: ud.Password,
                        role: Roles.Administrator);
                }
            }
        }

        if (mode.HasFlag(Init.InitializationMode.Users))
        {
            foreach (var ud in _users)
            {
                if ((await _userManager.FindByNameAsync(ud.Username)) == null)
                {
                    await ProvideUserWithRole(
                        user: new User()
                        {
                            UserName = ud.Username
                        },
                        pass: ud.Password,
                        role: Roles.User);
                }
            }
        }
    }
    /// <summary>
    ///     Provide user in user manager with chosen role
    /// </summary>
    /// <param name="user">
    ///     User whats been provided
    /// </param>
    /// <param name="role">
    ///     User role
    /// </param>
    /// <param name="pass">
    ///     User password
    /// </param>
    /// <returns>
    ///     Returns task of async operation by providing user with role
    /// </returns>
    private async Task ProvideUserWithRole(
        User user,
        string pass,
        Roles role)
    {
        await _userManager.CreateAsync(user, pass);
        await _userManager.AddToRoleAsync(user, role.ToString());
        await _claimProvider.ProvideRoleClaimAsync(user, role);
        await _claimProvider.ProvideAsync(user);
    }
}