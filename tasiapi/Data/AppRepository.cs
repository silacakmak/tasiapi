//using tasiapi.Repositories;
//using tasiapi.Models;
//using Microsoft.EntityFrameworkCore;
//using tasiapi.Data;
//using Npgsql;
//using Microsoft.AspNetCore.Mvc;

////using tasiapi.Interfaces;


//namespace tasiapi.Data
//{
//    //NpgsqlConnection Connection = new NpgsqlConnection("server=localhost;Port=5432;Database=tasinmaz;Username=postgres;Password=1234;");

//    public class AppRepository : IAppRepository
//    {
//        private TasinmazDbContext _context;



//        public AppRepository(TasinmazDbContext context)
//        {
//            _context = context;
//        }
//        //NpgsqlConnection _connection = new NpgsqlConnection("server=localhost;Port=5432;Database=tasinmaz;Username=postgres;Password=1234;");
//        public void Add<T>(T entity) where T : class
//        {
//            _context.Add(entity);
//            //throw new NotImplementedException();
//        }

//        public void Delete<T>(T entity) where T : class
//        {
//            _context.Remove(entity);
//            //throw new NotImplementedException();
//        }

//       public Tasinmaz GetTasinmazById(int id)

//        {
//            var tasinmazz = _context.tasinmazlar.FirstOrDefault(t => t.Id == id);//tek bir taşınmazı çekmeye yarar
//            return tasinmazz;
//            //throw new NotImplementedException();
//        }

//        public object GetTasinmazs()
//        {
//            throw new NotImplementedException();
//        }

//        bool SaveAll()
//        {
//            return _context.SaveChanges() > 0;//değişiklikleri kaydeder
//            //    throw new NotImplementedException();
//        }
//    }
//}
using System;
using System.Collections.Generic;
using System.Linq;
using tasiapi.Models;
using tasiapi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tasiapi.Interfaces;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace tasiapi.Data
{
    public class AppRepository : IAppRepository
    {
            private readonly TasinmazDbContext _context;

        public AppRepository(TasinmazDbContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public async Task Delete(Tasinmaz tasinmaz)
        {
            _context.tasinmazlar.Remove(tasinmaz);
            await _context.SaveChangesAsync();



        }
        public async Task Update(Tasinmaz tasinmaz)
        {
            var existingTasinmaz = await _context.tasinmazlar.FindAsync(tasinmaz.Id);

            if (existingTasinmaz == null)
            {
               
                return;
            }

            
            existingTasinmaz.Il_ad = tasinmaz.Il_ad;
            existingTasinmaz.Ilce_ad =tasinmaz.Ilce_ad;
            existingTasinmaz.Mahalle_ad =tasinmaz.Mahalle_ad;
            existingTasinmaz.Ada = tasinmaz.Ada;
            existingTasinmaz.Parsel = tasinmaz.Parsel;
            existingTasinmaz.UserId = tasinmaz.UserId;

            
            await _context.SaveChangesAsync();
        }

        //public void Delete<T>(T entity) where T : class
        //{
        //    return _context.tasinmazlar;
        //}

        public async Task<Tasinmaz>  GetTasinmazById(int id)
        {
            return  _context.tasinmazlar.FirstOrDefault(t => t.Id == id);//tek bir taşınmazı çekmeye yarar


        }

        public async Task<List<Tasinmaz>> GetTasinmazs()
        {
            return  _context.tasinmazlar.ToList();
            
        }

        public async Task<List<Tasinmaz>> GetTasinmazsAsync()
        {
            
            return  _context.tasinmazlar.ToList();
             
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        //public List<Tasinmaz> GetTasinmazs()
        //{
        //     var tasinmazs=_context.tasinmazlar.ToList();
        //    return tasinmazs;

        //}

        //public Task<List<Tasinmaz>> GetTasinmazs()
        //{
        //    return _context.tasinmazlar.ToListAsync();
        //}

        //public Tasinmaz GetTasinmazById(int id)
        //{
        //    return _context.tasinmazlar.Find(id);
        //}
    }
}
