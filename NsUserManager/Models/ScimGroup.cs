using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NsUserManager.Models;

public class ScimGroup
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("externalId")]
    public string? ExternalId { get; set; }

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; } = string.Empty;

    [JsonPropertyName("members")]
    public List<ScimGroupMember> Members { get; set; } = new();
}

public class AddScimGroup
{
    [Required]
    public string DisplayName { get; set; } = string.Empty;
}

public class ScimGroupMember
{
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;

    [JsonPropertyName("display")]
    public string Display { get; set; } = string.Empty;
} 