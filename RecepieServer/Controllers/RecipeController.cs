﻿using Microsoft.AspNetCore.Mvc;
using RecepieServer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecepieServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository repository;                                                                   

        public RecipeController(IRecipeRepository repository)
        {
            this.repository = repository; 
        }

        // GET: api/<RecipeController>
        [HttpGet]
        public IEnumerable<Recipe> Get()
        {
            return repository.GetAllRecipes();
        }

        // GET api/<RecipeController>/5
        [HttpGet("{id}")]
        public ActionResult<Recipe> Get(long id)
        {
            var recipe = repository.GetRecipeByID(id);

            if (recipe == null)
                return NotFound();

            return recipe;
        }

        // POST api/<RecipeController>
        [HttpPost]
        public void Post([FromBody] Recipe model)
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
