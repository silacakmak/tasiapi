//using tasiapi.Models;


//namespace tasiapi.Interfaces
//{
//    public interface IAppRepository
//    {
//        void Add<T>(T entity) where T :class;
//        void Delete<T>(T entity) where T : class;
//        bool SaveAll();
//        List<Tasinmaz> GetTasinmazs();//tüm taşınmazları getirecek
//        Tasinmaz GetTasinmazById(int id);//sadece belli bir taşınmazın datası

//    }
//}
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using tasiapi.Data;
using tasiapi.Interfaces;
using tasiapi.Models;

namespace tasiapi.Interfaces
{


    public class AppRepository : IAppRepository
    {

        private readonly TasinmazDbContext _context;

        public AppRepository(TasinmazDbContext context)
        {
            _context = context;
        }

        //        public void Add<T>(T entity) where T : class
        //        {
        //            _context.Set<T>().Add(entity);
        //        }

        //        public void Delete<T>(T entity) where T : class
        //        {
        //            _context.Set<T>().Remove(entity);
        //        }

        //        public bool SaveAll()
        //        {
        //            return _context.SaveChanges() > 0;
        //        }

        //    public List<Tasinmaz> GetTasinmazs()
        //    {
        //        return _context.tasinmazlar.ToList();
        //    }

        //public Tasinmaz GetTasinmazById(int id)
        //{

        //    return _context.tasinmazlar.Find(id);
        //}

        //     async Task<List<Tasinmaz>> GetTasinmazsAsync()
        //    {
        //        return _context.tasinmazlar.ToList();

        //    }
        //}
        void IAppRepository.Add<T>(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public async Task Delete(Tasinmaz tasinmaz)
        {
            _context.tasinmazlar.Remove(tasinmaz);
            await  _context.SaveChangesAsync();
        }
        //public async Task Update(Tasinmaz tasinmaz)
        //{

        //}
        public async Task Update(Tasinmaz tasinmaz)
        {
            var existingTasinmaz = await _context.tasinmazlar.FindAsync(tasinmaz.Id);

            if (existingTasinmaz == null)
            {
               
                return;
            }

            
            existingTasinmaz.Il_ad = tasinmaz.Il_ad;
            existingTasinmaz.Ilce_ad =tasinmaz.Ilce_ad;
            existingTasinmaz.Mahalle_ad = tasinmaz.Mahalle_ad;
            existingTasinmaz.Ada = tasinmaz.Ada;
            existingTasinmaz.Parsel = tasinmaz.Parsel;
            existingTasinmaz.UserId = tasinmaz.UserId;

            
            await _context.SaveChangesAsync();
        }
        async Task<List<Tasinmaz>> IAppRepository.GetTasinmazs()
        {
            return _context.tasinmazlar.ToList();
        }

        async Task<List<Tasinmaz>> IAppRepository.GetTasinmazsAsync()
        {
            return await _context.tasinmazlar.ToListAsync();
        }
        async Task<Tasinmaz> IAppRepository.GetTasinmazById(int id)
        {
            return  _context.tasinmazlar.Find(id);
        }

        bool IAppRepository.SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
    
    