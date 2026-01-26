using TodoList.Dtos;

namespace TodoList.Services
{
    public class UserService : IUserService
    {
        public Task<UserOutput> AddUser(UserImput userImput)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUSerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserOutput> GetAllUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserOutput> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserAsync(UserImput userImput, int id)
        {
            throw new NotImplementedException();
        }
    }
}
