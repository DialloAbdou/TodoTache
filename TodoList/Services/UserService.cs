using Microsoft.EntityFrameworkCore;
using System.Collections;
using TodoList.Data;
using TodoList.Dtos;
using TodoList.Models;
using TodoList.Tools;

namespace TodoList.Services
{
    public class UserService : IUserService
    {
        private readonly TacheDbContext _dbContext;

        public UserService(TacheDbContext tacheDbContext)
        {
            _dbContext = tacheDbContext;
        }

        public async Task<UserOutput> AddUserAsync(UserImput userImput)
        {
            var user = new User
            {
                Nom = userImput.Name,
                Token = Outils.GenerateCode8()
            };
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
             var userOut =  GetUserOutput(user);
            return userOut;
        }

        public async Task<bool> DeleteUSerAsync(int id)
        {
            var result = await _dbContext.Users
                            .Where(u => u.Id == id)
                            .ExecuteDeleteAsync();
            return result > 0;
        }

        public async Task<IEnumerable<UserOutput>> GetAllTacheAsync()
        {
            var _user = (await _dbContext.Users.ToListAsync())
                     .Select(u => GetUserOutput(u)).ToList();
            return _user;
        }

        public async Task<UserOutput?> GetUserByIdAsync(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user == null ? null : GetUserOutput(user);

        }
        public async Task<bool> UpdateUserAsync(UserImput userImput, int id)
        {
            var result = await _dbContext.Users.Where(u => u.Id == id)
                 .ExecuteUpdateAsync(setter =>
                   setter
                   .SetProperty(t => t.Nom, userImput.Name)
                   .SetProperty(t => t.Token, userImput.Token));

            return result > 0;

        }

        private User GetUser(UserImput userImput)
        {
            return new User
            {
                Nom = userImput.Name,
                Token = userImput.Token!
            };
        }

        private UserOutput GetUserOutput(User user)
        {
            return new UserOutput
                (
                   user.Id,
                   user.Nom,
                    user.Token,
                    user.Taches
                );
        }
    }
}
