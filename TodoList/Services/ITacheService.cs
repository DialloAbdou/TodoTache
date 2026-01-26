using TodoList.Dtos;

namespace TodoList.Services
{
    public interface ITacheService
    {
         Task<IEnumerable<TacheOutput>> GetAllTacheAsync();
         Task<TacheOutput> AddTacheAsync(TacheImput tacheImput);
         Task<TacheOutput?> GeTacheById(int id);
         Task<bool> UpdateTacheAsync(int  id, TacheImput tacheImpute);
         Task<bool> DeleteTacheAsync(int id);
 
    }
}
