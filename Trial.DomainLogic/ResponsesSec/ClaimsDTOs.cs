namespace Trial.DomainLogic.ResponsesSec;

public class ClaimsDTOs
{
    public string Email { get; set; } = default!;
    public string Id { get; set; } = default!;
    public string Role { get; set; } = default!;
    public int CorporationId { get; set; }
}