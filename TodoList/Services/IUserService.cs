using TodoList.Dtos;

namespace TodoList.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserOutput>> GetAllTacheAsync();
        Task<UserOutput?> GetUserByIdAsync(int id);
        Task<UserOutput> AddUserAsync(UserImput userImput);
        Task<bool> UpdateUserAsync(UserImput userImput, int id);
        Task<bool> DeleteUSerAsync(int id);
    }
}
