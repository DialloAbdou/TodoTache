using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TodoList.Data;
using TodoList.Dtos;
using TodoList.Models;

namespace TodoList.Services
{
    public class TacheService : ITacheService
    {

        public TacheDbContext _tacheDbContext { get; }
        public TacheService(TacheDbContext tacheDbContext)
        {
            _tacheDbContext = tacheDbContext;
        }
        public async Task<IEnumerable<TacheOutput>> GetAllTacheAsync()
        {
            var taches = (await _tacheDbContext.Taches.ToListAsync())
                .Select(t => GetTacheOutput(t)).ToList();
            return taches;
        }


        public async Task<TacheOutput> AddTacheAsync(string titre, string tokenUser)
        {
            User? user = await _tacheDbContext.Users.FirstOrDefaultAsync(u => u.Token == tokenUser);
            if (user == null) return null;
            var tache = new Tache
            {
                Titre =titre,
                DateDebut = DateTime.Now,
                UserId = user.Id,
            };
            _tacheDbContext.Taches.Add(tache);
     
          await  _tacheDbContext.SaveChangesAsync();

            return GetTacheOutput(tache);                 
        }

        public async Task<TacheOutput?> GeTacheById(int id)
        {
            var tache = await _tacheDbContext.Taches.FirstOrDefaultAsync(t => t.Id == id);
            if (tache is null) return null;
            return GetTacheOutput(tache);
        }

        private Tache GetTache(TacheImput imput)
        {
            return new Tache
            {
                Titre = imput.Titre,
                DateDebut = imput.DateDebut ?? DateTime.Today,
                DateFin = imput.DateFin
            };
        }

        public TacheOutput GetTacheOutput(Tache tache)
        {
            return new TacheOutput
                (
                   titre: tache.Titre,
                    DateDebut: tache.DateDebut,
                    DateFIn: tache.DateFin
                );
        }

        public async Task<bool> UpdateTacheAsync(int id, TacheImput tacheImput)
        {
            var _result = await _tacheDbContext.Taches.Where(t => t.Id == id)
               .ExecuteUpdateAsync(s => s.SetProperty(t => t.Titre, tacheImput.Titre)
               .SetProperty(t => t.DateDebut, tacheImput.DateDebut)
               .SetProperty(t => t.DateFin, tacheImput.DateFin));
            return _result > 0;

        }

        public async Task<bool> DeleteTacheAsync(int id)
        {
            var _result = await _tacheDbContext.Taches
               .Where(t => t.Id == id).ExecuteDeleteAsync();
            return _result > 0;
        }

        /// <summary>
        /// Verifier 
        /// si l'utilisateur lie à cette tache
        /// </summary>
        /// <param name="tokenUser"></param>
        /// <returns></returns>
        public async Task<bool> USerIsExist(string tokenUser)
        {
            var taches = await _tacheDbContext.Taches
                .Include(t=>t.User).Where(t=>t.User.Token == tokenUser).ToListAsync();
             if( taches.Any() ) return true;
             return false;
        }

        /// <summary>
        /// elle verifie ce token existe dans la base de donnée
        /// </summary>
        /// <param name="tolenUser"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> USerIsValid(string tolenUser)
        {
            var isExist = await _tacheDbContext.Users.FirstOrDefaultAsync(u=>u.Token == tolenUser);
            if( isExist == null ) return false;
            return true;
        }
    }
}
