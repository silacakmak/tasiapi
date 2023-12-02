//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace tasiapi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ValuesController : ControllerBase
//    {

//    }
//}
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using tasiapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using tasiapi.Dtos;
using AutoMapper;

namespace tasiapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private TasinmazDbContext _context;
        public ValuesController(TasinmazDbContext context)
        {
            _context = context;
           
        }

        // GET: api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tasinmaz>>> GetTasinmazs()
        {
            //return new string[] { "value1", "value2" };
            var values = await _context.tasinmazlar.ToListAsync();
         
            return Ok(value: values);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

