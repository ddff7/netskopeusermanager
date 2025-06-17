using NsUserManager.Models;

namespace NsUserManager.Services;

public interface IScimService
{
    Task<List<ScimUser>> GetUsersAsync();
    Task<ScimUser?> GetUserAsync(string id);
    Task<ScimUser> CreateUserAsync(AddScimUser user);
    Task<ScimUser> UpdateUserAsync(string id, ScimUser user);
    Task UpdateUserStateAsync(ScimUser user);
    
    Task DeleteUserAsync(string id);

    // Group management
    Task<List<ScimGroup>> GetGroupsAsync();
    Task<ScimGroup?> GetGroupAsync(string id);
    Task<ScimGroup> CreateGroupAsync(AddScimGroup group);
    Task<ScimGroup> UpdateGroupAsync(string id, ScimGroup group);
    Task DeleteGroupAsync(string id);
    Task PatchGroupMembersAsync(string groupId, List<ScimGroupMember> membersToAdd, List<ScimGroupMember> membersToRemove);
}
