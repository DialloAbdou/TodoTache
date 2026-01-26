using TodoList.Dtos;

namespace TodoList.Services
{
    public interface IUserService
    {
        Task<UserOutput> GetAllUserAsync();
        Task<UserOutput> GetUserByIdAsync(int id);
        Task<UserOutput> AddUser(UserImput userImput);
        Task<bool> UpdateUserAsync(UserImput userImput, int id);
        Task<bool> DeleteUSerAsync(int id);
    }
}
