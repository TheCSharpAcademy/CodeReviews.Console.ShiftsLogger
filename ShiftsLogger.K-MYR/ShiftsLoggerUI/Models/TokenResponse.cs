namespace ShiftsLoggerUI;

class TokenResponse
{
    public string TokenType { get; set; }
    public string AccessToken { get; set; }
    public int ExpiresIn { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string RefreshToken { get; set; }

    public TokenResponse(string tokenType, string accessToken, int expiresIn, string refreshToken)
    {
        TokenType = tokenType;
        AccessToken = accessToken;
        ExpiresIn = expiresIn;
        ExpirationDate = DateTime.UtcNow + TimeSpan.FromSeconds(ExpiresIn);
        RefreshToken = refreshToken;
    }
}
