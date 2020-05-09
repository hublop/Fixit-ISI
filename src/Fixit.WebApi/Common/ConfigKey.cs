namespace Fixit.WebApi.Common
{
    public static class ConfigKey
    {
        public static readonly string RequireHttpsMetadataSource = "JsonWebToken:Bearer:RequireHttpsMetadata";
        public static readonly string IncludeErrorDetailsSource = "JsonWebToken:Bearer:IncludeErrorDetails";
        public static readonly string JwtAudienceSource = "JsonWebToken:TokenOptions:Audience";
        public static readonly string JwtIssuerSource = "JsonWebToken:TokenOptions:Issuer";
        public static readonly string JwtKeySource = "JsonWebToken:TokenOptions:Key";
        public static readonly string JwtTokenExpiryTimeInMinutesSource = "JsonWebToken:TokenOptions:TokenExpiryTimeInMinutes";
        public static readonly string RefreshTokenExpiryTimeInMinutesSource = "JsonWebToken:TokenOptions:RefreshTokenExpiryTimeInMinutes";
    }
}