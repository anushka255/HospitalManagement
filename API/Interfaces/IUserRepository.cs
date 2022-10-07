using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces;

public interface IUserRepository
{
    Task<bool> SaveAllAsync();
    Task<IEnumerable<AppUser>> GetUsersAsync();
    Task<AppUser> GetUserByUsernameAsync(string username);

    Task<PagedList<MemberDTO>> GetMembersAsync(UserParams userParams);
    Task<AppUser> GetUserByIdAsync(int id);

    Task<MemberDTO> GetMemberAsync(string username);
    void Update(AppUser user);
    Task<PagedList<MemberDTO>> GetEmployeeAsync(EmployeeParam userParams);
}