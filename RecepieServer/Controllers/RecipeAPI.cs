using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecepieServer.Models;
using RecepieServer.Repository;
using RecepieServer.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecepieServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository _repository;
        private readonly IStorageService _storage;
       
        public RecipeController(IRecipeRepository repository, IStorageService storage)
        {
            _repository = repository;
            _storage = storage;
        }

        // GET: api/<RecipeController>
        [HttpGet]
        public IEnumerable<Recipe> Get()
        {
            return _repository.GetAllRecipes();
        }

        // GET api/<RecipeController>/5
        [HttpGet("{id}")]
        public ActionResult<RecipeDetails> Get(long id)
        {
            var recipe = _repository.GetRecipeByID(id);
            if (recipe == null)
                return NotFound();

            var details = _repository.GetRecipeDetails(recipe);
            if (details == null)
                return NotFound();

            return details;
        }

        [HttpGet("{id}/img/{path}")]
        public ActionResult<byte[]> Get(long id,string path)
        {
            var recipe = _repository.GetRecipeByID(id);
            if (recipe == null)
                return NotFound();

            var resource = _repository.GetResourceForRecipe(id, path);
            if (resource == null)
                return NotFound();

            return resource.Result;
        }
        // POST api/<RecipeController>
        // WE USE THIS ROUTE TO UPLOAD PICTURES
        [HttpPost]
        [Route("{id}/upload")]
        public IActionResult Post(long id,IFormFile file)
        {
           _storage.Upload(file,id);
           return Ok();
        }

        [HttpPost]
        [Route("{id}")]
        public void Post(long id,RecipeDetails recipe)
        {
            throw new NotImplementedException();
        }

        // PUT api/<RecipeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<RecipeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
