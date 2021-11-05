using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RecipesApp.Models;
using System.Collections.Generic;
using System.IO;

namespace RecipesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public RecipeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public List<Recipe> getRecipes()
        {
            StreamReader r = new StreamReader("recipes.json");
            string jsonString = r.ReadToEnd();
            List<Recipe> recipes = DeserializeRecipes(jsonString);
            return recipes;
        }

        private List<Recipe> DeserializeRecipes(string json)
        {
            List<Recipe> recipes = new List<Recipe>();
            JToken token = JToken.Parse(json);
            for (int i = 1;i < 11; i++)
            {
                JObject jObj = (JObject)token.SelectToken(i.ToString());
                Recipe recipe = jObj.ToObject<Recipe>();
                recipes.Add(recipe);
            }
            return recipes;
        }
    }
}
