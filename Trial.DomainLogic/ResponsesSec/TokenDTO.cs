﻿namespace Trial.DomainLogic.ResponsesSec;

public class TokenDTO
{
    public string Token { get; set; } = null!;

    public DateTime Expiration { get; set; }
}