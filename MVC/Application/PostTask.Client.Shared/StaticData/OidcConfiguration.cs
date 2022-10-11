namespace PostTask.Client.Shared.StaticData;
/// <summary>
///     Oidc configuration static data
/// </summary>
public static class OidcConfiguration
{
    public static string ClientId { get; set; } = string.Empty;
    public static string ClientSecret { get; set; } = string.Empty;
    public static IEnumerable<string> Scopes { get; set; } = Enumerable.Empty<string>();
}