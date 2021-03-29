using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecepieServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        // GET: api/<RecipeController>
     

        [HttpGet]
        public IEnumerable<Recipe> Get()
        {
            Recipe[] recipes = new Recipe[0];


            return new Recipe[] { new Recipe(){Name = "Tort" }, new Recipe() { Name = "Praji"} };
        }

        // GET api/<RecipeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RecipeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RecipeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RecipeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
