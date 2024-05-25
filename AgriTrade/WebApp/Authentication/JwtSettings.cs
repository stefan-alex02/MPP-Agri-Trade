namespace WebApp.Authentication;

public class JwtSettings {
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }
    public TimeSpan TokenLifetime { get; set; }
    public TimeSpan RefreshWindow { get; set; }
}