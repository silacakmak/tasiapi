//using AutoMapper;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using tasiapi.Dtos;
//using tasiapi.Repositories;
//using tasiapi.Data;

//namespace tasiapi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TasinmazsController : ControllerBase
//    {
//        private IAppRepository _appRepository;
//        private IMapper _mapper;



//        public TasinmazsController(IAppRepository appRepository, IMapper mapper)
//        {
//            _appRepository = appRepository;
//            _mapper = mapper;
//        }

//        [HttpGet("test")]
//        public ActionResult GetTasinmazs()
//        {


//var tasinmazs = _appRepository.GetTasinmazs().
//            var tasinmazsToReturn = _mapper.Map<List<TasinmazForListDto>>(tasinmazs);

//Select(t => new TasinmazForListDto { Il_ad = t.Il_ad, Ilce_ad = t.Ilce_ad, Mahalle_ad = t.Mahalle_ad, Ada = t.Ada, Parsel = t.Parsel,Id=t.Id }).ToList();
//return Ok(tasinmazs);
//        }
//    }

//using Microsoft.AspNetCore.Http;
//using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
//using tasiapi.Data;
//using Npgsql.EntityFrameworkCore.PostgreSQL;
using tasiapi.Dtos;

using Npgsql;
//using NpgsqlTypes;
//using AutoMapper.EntityFrameworkCore;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tasiapi.Interfaces;
//using AutoMapper.QueryableExtensions;
//using tasiapi.Helpers;
using tasiapi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
//using System.Data;

namespace tasiapi.Controllers
{
    [Route("api/test1")]
    [ApiController]
    public class TasinmazsController : ControllerBase
    {
        private readonly IAppRepository _appRepository;
        //private readonly IMapper _mapper;

        public TasinmazsController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
            //_mapper = mapper;
        }
        NpgsqlConnection _connection = new NpgsqlConnection("Host=localhost;Port=5432;Database=tasinmaz;Username=postgres;Password=1234");


        [HttpGet("tasinmazlar")]


        public async Task<ActionResult> GetTasinmazsAsync()
        {
            //string sorgu = "select * from tasinmazlar";
            //NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sorgu,_connection);
            //DataSet ds = new DataSet();
            //return Ok(adapter.Fill(ds));


            var tasinmazs = await _appRepository.GetTasinmazsAsync();
            var x = tasinmazs.Select(t => new TasinmazForListDto { Il_ad = t.Il_ad, Ilce_ad = t.Ilce_ad, Mahalle_ad = t.Mahalle_ad, Ada = t.Ada, Parsel = t.Parsel, UserId = t.UserId, Id = t.Id })
                    .ToList();
            return Ok(x);



        }
        [HttpPost]
        [Route("add")]
        public ActionResult Add([FromBody] Tasinmaz tasinmaz)
        {
            _appRepository.Add(tasinmaz);
            _appRepository.SaveAll();
            return Ok(tasinmaz);
        }
        [HttpDelete("id")]
        public async Task<StatusCodeResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var tasinmaz = await _appRepository.GetTasinmazById(id);

            if (tasinmaz == null)
            {
                return NotFound();
            }

            await _appRepository.Delete(tasinmaz);

            return StatusCode(201);
        }

       
            [HttpPut()]
            public async Task<IActionResult> Update(int id, [FromBody] Tasinmaz tasinmaz)
            {
                if (id != tasinmaz.Id)
                {
                    return BadRequest();
                }

                try
                {
                    await _appRepository.Update(tasinmaz);
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                    return NotFound();
                }

                return NoContent();
            }

            
        


        [HttpGet]
        [Route("birtasi")]

        public async Task<ActionResult> GetTasinmazById(int id)
        {



            var tasinmazs = await _appRepository.GetTasinmazById(id);
            //var x = _mapper.Map<TasinmazForDetailDto>(tasinmazs);

            //var x = new TasinmazForListDto
            //{ Il_ad = tasinmazs.Il_ad, Ilce_ad = tasinmazs.Ilce_ad, Mahalle_ad = tasinmazs.Mahalle_ad, Ada = tasinmazs.Ada, Parsel = tasinmazs.Parsel, Id = tasinmazs.Id };
            Console.WriteLine(tasinmazs);

            var dto = new TasinmazForDetailDto
            {
                Il_ad = tasinmazs.Il_ad,
                Ilce_ad = tasinmazs.Ilce_ad,
                Mahalle_ad = tasinmazs.Mahalle_ad,
                Ada = tasinmazs.Ada,
                Parsel = tasinmazs.Parsel,
                UserId = tasinmazs.UserId,
                Id = tasinmazs.Id
            };

            //var x = tasinmazs.Select(t => new TasinmazForDetailDto { Il_ad = t.Il_ad, Ilce_ad = t.Ilce_ad, Mahalle_ad = t.Mahalle_ad, Ada = t.Ada, Parsel = t.Parsel, Id = t.Id });
            //.ToList();
            return Ok(dto);



        }
    }
}



    


