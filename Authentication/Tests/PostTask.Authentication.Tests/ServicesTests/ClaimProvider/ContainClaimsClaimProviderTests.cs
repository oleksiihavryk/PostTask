using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using PostTask.Authentication.Core.ClaimProvider;
using PostTask.Authentication.Core.Exceptions;
using PostTask.Authentication.Domain;

namespace PostTask.Authentication.Tests.ServicesTests.ClaimProvider;
public class ContainClaimsClaimProviderTests
{
    private readonly IDictionary<User, ICollection<Claim>?> _userClaimStore
        = new Dictionary<User, ICollection<Claim>?>();

    [Fact]
    public async Task ContainClaimsClaimProvider_ProvideClaims_ExistedUser()
    {
        //arrange
        var user = new User()
        {
            UserName = "Pavlo",
        };
        var um = GetUserManager(new List<User> { user });
        var provider = new ContainClaimsClaimProvider(um);

        //act
        await provider.ProvideAsync(user);

        //assert
        _userClaimStore.TryGetValue(user, out var claims);
        Assert.NotNull(claims);
        foreach (var s in provider.ClaimsAndActions)
        {
            Assert.Contains(
                expected: s.Key, 
                collection: claims?.Select(c => c.Type) ?? 
                            throw new InvalidOperationException("Impossible error"));
            Assert.Equal(
                expected: s.Value(user),
                actual: claims.FirstOrDefault(c => c.Type == s.Key)?.Value);
        }
    }
    [Fact]
    public async Task ContainClaimsClaimProvider_ProvideClaims_NotExistedUser()
    {
        async Task Test()
        {
            //arrange
            var user = new User()
            {
                UserName = "Pavlo",
            };
            var um = GetUserManager(new List<User>());
            var provider = new ContainClaimsClaimProvider(um);

            //act
            await provider.ProvideAsync(user);
        }

        await Assert.ThrowsAsync<UserNotFoundException>(Test);
    }
    [Fact]
    public async Task ContainClaimsClaimProvider_ProvideUserRoleClaim_NotExistedUser()
    {
        async Task Test()
        {
            //arrange
            var user = new User()
            {
                UserName = "basharassadhuiy"
            };
            var um = GetUserManager(new List<User>());
            var provider = new ContainClaimsClaimProvider(um);

            //act
            await provider.ProvideRoleClaimAsync(user, Roles.User);
        }

        //assert
        await Assert.ThrowsAsync<UserNotFoundException>(Test);
    }
    [Fact]
    public async Task ContainClaimsClaimProvider_ProvideUserRoleClaim_ExistedUser()
    {
        //arrange
        var user = new User()
        {
            UserName = "basharassadhuiy"
        };
        var um = GetUserManager(new List<User> { user });
        var provider = new ContainClaimsClaimProvider(um);
        var role = Roles.User;

        //act
        await provider.ProvideRoleClaimAsync(user, role);

        //assert
        var claims = _userClaimStore[user];
        Assert.NotNull(claims);
        Assert.Contains(
            expected: provider.RoleClaimType,
            collection: claims?.Select(c => c.Type) ??
                        throw new InvalidOperationException(
                            message: "Impossible error. Correct test."));
        Assert.Equal(
            expected: claims.FirstOrDefault(c => c.Type == provider.RoleClaimType)?.Value,
            actual: role.ToString()); 
    }

    private UserManager<User> GetUserManager(List<User> userList)
    {
        var store = new Mock<IUserStore<User>>();
        var userManager = new Mock<UserManager<User>>(
            args: new object?[] { store.Object, null, null, null, null, null, null, null, null });

        foreach (var u in userList)
            _userClaimStore[u] = new List<Claim>();

        userManager
            .Setup(um => um.AddClaimsAsync(
                It.IsAny<User>(), 
                It.IsAny<ICollection<Claim>>()))
            .Callback<User, IEnumerable<Claim>>(
                (u, c) => _userClaimStore[u] = c.ToList())
            .Returns(Task.FromResult(IdentityResult.Success));
        userManager
            .Setup(um => um.AddClaimAsync(
                It.IsAny<User>(),
                It.IsAny<Claim>()))
            .Callback<User, Claim>(
                (u, c) => _userClaimStore[u]?.Add(c))
            .Returns(Task.FromResult(IdentityResult.Success));
        userManager
            .Setup(um => um.FindByNameAsync(It.IsAny<string>()))
            .Returns<string>(
                name => Task.FromResult(
                    _userClaimStore.Keys.FirstOrDefault(u => u.UserName == name)));
        return userManager.Object;
    }
}