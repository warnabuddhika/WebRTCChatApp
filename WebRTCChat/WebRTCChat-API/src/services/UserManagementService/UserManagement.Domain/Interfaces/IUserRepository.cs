using UserManagement.Domain.Dtos;
using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Interfaces;

public interface IUserRepository 
{
	Task<AppUser> GetUserByIdAsync(Guid id);
	Task<AppUser> GetUserByUsernameAsync(string username);
	Task<MemberDto> GetMemberAsync(string username);	
	Task<IEnumerable<MemberDto>> SearchMemberAsync(string displayname);
	Task<IEnumerable<MemberDto>> GetUsersOnlineAsync(UserConnectionInfo[] userOnlines);
	Task<AppUser> UpdateLocked(string username);
}
