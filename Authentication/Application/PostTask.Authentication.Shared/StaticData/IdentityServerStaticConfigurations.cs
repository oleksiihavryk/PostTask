using System.ComponentModel;

namespace PostTask.Authentication.Shared.StaticData;
/// <summary>
///     Identity server application configurations
/// </summary>
public static class IdentityServerStaticConfigurations
{
    /// <summary>
    ///     Mvc client
    /// </summary>
    public static IdentityClient Mvc { get; private set; } = new IdentityClient(
        id: string.Empty,
        secret: string.Empty,
        scopes: Array.Empty<string>());

    /// <summary>
    ///     Setup chosen client
    /// </summary>
    /// <param name="opt">
    ///     Action for setup and identity client
    /// </param>
    /// <param name="client">
    ///     Client what has been configured
    /// </param>
    public static void SetupClient(
        Action<IdentityClientBuilder> opt,
        IdentityClientType client)
    {
        var builder = new IdentityClientBuilder();
        opt(builder);

        switch (client)
        {
            case IdentityClientType.Mvc:
            {
                Mvc = builder.Build();
                break;
            }
            default:
                throw new InvalidEnumArgumentException(
                    message: "Current client enum handling is unavailable.");
        }
    }

    /// <summary>
    ///     Model of identity client 
    /// </summary>
    public class IdentityClient
    {
        /// <summary>
        ///     Client identifier
        /// </summary>
        public string Id { get; }

        /// <summary>
        ///     Client secret
        /// </summary>
        public string Secret { get; }

        /// <summary>
        ///     Client scopes
        /// </summary>
        public ICollection<string> Scopes { get; }

        public IdentityClient(
            string id,
            string secret,
            ICollection<string> scopes)
        {
            Id = id;
            Secret = secret;
            Scopes = scopes;
        }
    }
    /// <summary>
    ///     Identity client builder
    /// </summary>
    public class IdentityClientBuilder
    {
        /// <summary>
        ///     Client id
        /// </summary>
        private string? _id = null;

        /// <summary>
        ///     Client secret
        /// </summary>
        private string? _secret = null;

        /// <summary>
        ///     Client scopes
        /// </summary>
        private List<string> _scopes = new List<string>();

        /// <summary>
        ///     Set identifier to identity client
        /// </summary>
        /// <param name="identifier">
        ///     Client identifier
        /// </param>
        /// <returns>
        ///     Returns itself
        /// </returns>
        public IdentityClientBuilder SetId(string identifier)
        {
            _id = identifier;
            return this;
        }
        /// <summary>
        ///     Set secret to identity client
        /// </summary>
        /// <param name="secret">
        ///     Client secret
        /// </param>
        /// <returns>
        ///     Returns itself
        /// </returns>
        public IdentityClientBuilder SetSecret(string secret)
        {
            _secret = secret;
            return this;
        }
        /// <summary>
        ///     Adds scope to identity client
        /// </summary>
        /// <param name="scope">
        ///     Client scope
        /// </param>
        /// <returns>
        ///     Returns itself
        /// </returns>
        public IdentityClientBuilder AddScope(string scope)
        {
            _scopes.Add(scope);
            return this;
        }
        /// <summary>
        ///     Build identity client   
        /// </summary>
        /// <returns>
        ///     Created identity client
        /// </returns>
        public IdentityClient Build()
        {
            return new IdentityClient(
                id: _id ?? throw new ArgumentNullException(
                    message: "Client id is not configured",
                    paramName: nameof(_id)),
                secret: _secret ?? throw new ArgumentNullException(
                    message: "Client secret is not configured",
                    paramName: nameof(_secret)),
                scopes: _scopes.Any() ? 
                    _scopes : throw new ArgumentNullException(
                        message: "Client scopes is not configured",
                        paramName: nameof(_scopes)));
        }
    }
    /// <summary>
    ///     Identity client type (for chosen which identity client type is configure)
    /// </summary>
    public enum IdentityClientType
    {
        /// <summary>
        ///     Mvc client
        /// </summary>
        Mvc,   
    }
}