﻿using Newtonsoft.Json;

namespace Trial.AppBack.LoadCountries;

public class CityResponse
{
    [JsonProperty("id")]
    public long CityId { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }
}