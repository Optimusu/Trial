namespace Trial.DomainLogic.ResponsesSec;

public class UserClaimsInfo
{
    public string Email { get; set; } = default!;
    public string Id { get; set; } = default!;
    public string Role { get; set; } = default!;
    public string? CorporateId { get; set; }
}