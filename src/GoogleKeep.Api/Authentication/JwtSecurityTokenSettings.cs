namespace GoogleKeep.Api.Authentication
{
    public class JwtSecurityTokenSettings
    {
        public string Issuer { get; }
        public string Audience { get; }
        public byte[] SigningKey { get; }

        public TimeSpan Lifespan { get; }

        public JwtSecurityTokenSettings(string issuer, string audience, byte[] signingKey, TimeSpan? lifespan = null)
        {
            this.Issuer = issuer;
            this.Audience = audience;
            this.SigningKey = signingKey;
            this.Lifespan = lifespan ?? TimeSpan.FromHours(1);
        }
    }
}
