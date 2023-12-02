using System.Collections.Generic;
using tasiapi.Dtos;
using tasiapi.Models;

namespace tasiapi.Interfaces
{
    public interface IAppRepository
    {
        void Add<T>(T entity) where T : class;
        //void Delete<T>(T entity) where T : class;
        //public Delete <T>(T entity) where T : class;
        public Task Delete(Tasinmaz tasinmaz);
        public Task Update(Tasinmaz tasinmaz);
        bool SaveAll();
        public Task<List<Tasinmaz>> GetTasinmazsAsync();
        Task<List<Tasinmaz>> GetTasinmazs();
         public Task<Tasinmaz> GetTasinmazById(int id);
        
        /* Task<List<Tasinmaz>> GetTasinmazsAsync(); */// Tüm taşınmazları getirecek
                                                       //Tasinmaz GetTasinmazById(int id); // Sadece belli bir taşınmazın datası
    }
}