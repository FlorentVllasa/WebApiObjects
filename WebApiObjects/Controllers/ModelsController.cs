using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiObjects.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;

namespace WebApiObjects.Controllers
{
    public class ModelsController : ODataController
    {
        private WebDbContext _dbContext;
        public ModelsController(WebDbContext context)
        {
            _dbContext = context;

        }

        public string SayHello()
        {
            return "Hello";
        }

        public void AddSampleData()
        {

            List<Property> properties = new List<Property>();
            

            Model Salami = new Model
            {
                Name = "Salami",
                SubModel = null,
                Properties = properties
            };

            Property SalamiMeat = new Property
            {
                Name = "Salami Meat",
                ParentModel = Salami
            };

            properties.Add(SalamiMeat);

            List<Model> CheeseModels = new List<Model>();

            Model Cheese = new Model
            {
                Name = "Cheese",
                SubModel = CheeseModels
            };

            Model CheeseShape = new Model
            {
                Name = "CheeseShape",
                SubModel = null
            };

            Model CheeseOrigin = new Model
            {
                Name = "CheeseOrigin",
                SubModel = null
            };

            CheeseModels.Add(CheeseShape);
            CheeseModels.Add(CheeseOrigin);

            List<Model> Ingredients = new List<Model>();
            Ingredients.Add(Salami);
            Ingredients.Add(Cheese);

            Model Pizza = new Model
            {
                Name = "pizza",
                SubModel = Ingredients
            };

            _dbContext.Add(Pizza);
            _dbContext.Add(Salami);
            _dbContext.Add(SalamiMeat);
            _dbContext.Add(Cheese);
            _dbContext.SaveChanges();
        }

        public string RetrieveModels()
        {
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Error = (sender, args) =>
                {
                    args.ErrorContext.Handled = true;
                },

            };

            //var pizza = _dbContext.Models
            //    .Where(m => m.Name.Equals("pizza"))
            //    .Include(m => m.SubModel)
            //    .ThenInclude(m => m.Properties);

            var pizza = _dbContext.Models
                .Where(m => m.Name.Equals("pizza"))
                .Select(m => new Model()
                {
                    SubModel = _dbContext.Models.Include(m => m.SubModel).ThenInclude(m => m.Properties).ToList(),
                    Properties = _dbContext.Properties.Where(p => p.ParentModel.ID == m.ID).ToList()

                });

            return JsonConvert.SerializeObject(pizza, settings);

        }

        [HttpGet]
        [EnableQuery]
        public IEnumerable<Model> Get()
        {
            return _dbContext.Models;//.Where(m => m.ID == key).Include(m => m.SubModel);
        }

    }
}
