using System.Text.Json.Serialization;

namespace NsUserManager.Models;

public class ScimUser
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("externalId")]
    public string? ExternalId { get; set; }

    [JsonPropertyName("userName")]
    public string UserName { get; set; } = string.Empty;

    [JsonPropertyName("active")]
    public bool Active { get; set; } = true;

    [JsonPropertyName("name")]
    public Name? Name { get; set; }

    [JsonPropertyName("emails")]
    public List<Email> Emails { get; set; } = new();

    [JsonPropertyName("groups")]
    public List<Group> Groups { get; set; } = new();
}

public class Name
{
    [JsonPropertyName("givenName")]
    public string? GivenName { get; set; }

    [JsonPropertyName("familyName")]
    public string? FamilyName { get; set; }
}

public class Email
{
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;

    [JsonPropertyName("primary")]
    public bool Primary { get; set; }
}

public class Group
{
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;

    [JsonPropertyName("display")]
    public string Display { get; set; } = string.Empty;
} 