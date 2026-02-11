using TodoList.Dtos;

namespace TodoList.Services
{
    public interface ITacheService
    {
         Task<IEnumerable<TacheOutput>> GetAllTacheAsync();
         Task<TacheOutput> AddTacheAsync(string titre, string tokenUser);
         Task<TacheOutput?> GeTacheById(int id);
         Task<bool> UpdateTacheAsync(int  id, TacheImput tacheImput);
         Task<bool> DeleteTacheAsync(int id);
        Task<bool> USerIsExist(string tokenUser);
        Task<bool> USerIsValid(string tolenUser);
 
    }
}
