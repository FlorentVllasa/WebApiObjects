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
using System.Diagnostics;
using System.Linq.Expressions;
using WebApiObjects.HelperModels;

namespace WebApiObjects.Controllers
{
    public class ModelsController : ODataController
    {
        private WebDbContext _dbContext;
        public ModelsController(WebDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public string GetModels()
        {
            var AllModels = _dbContext.Models.ToList();

            foreach (Model model in AllModels)
            {
                LoadRecursively(model);
            }

            var JsonSettings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            return JsonConvert.SerializeObject(AllModels, JsonSettings);
        }

        public void AddSampleData()
        {

            List<Property> properties = new List<Property>();

            Project TestProject = new Project
            {
                Name = "TestProject",
                //Models = AllModels
            };

            Model Salami = new Model
            {
                Name = "Salami",
                SubModels = null,
                Properties = properties,
                ProjectId = TestProject
            };

            Property SalamiMeat = new Property
            {
                Name = "Salami Meat",
            };

            properties.Add(SalamiMeat);

            List<Model> CheeseModels = new List<Model>();

            Model Cheese = new Model
            {
                Name = "Cheese",
                SubModels = CheeseModels,
                ProjectId = TestProject
            };

            Model CheeseShape = new Model
            {
                Name = "CheeseShape",
                SubModels = null,
                ProjectId = TestProject
            };

            Model CheeseOrigin = new Model
            {
                Name = "CheeseOrigin",
                SubModels = null,
                ProjectId = TestProject
            };

            CheeseModels.Add(CheeseShape);
            CheeseModels.Add(CheeseOrigin);

            List<Model> Ingredients = new List<Model>();
            Ingredients.Add(Salami);
            Ingredients.Add(Cheese);

            Model Pizza = new Model
            {
                Name = "pizza",
                SubModels = Ingredients,
                ProjectId = TestProject
            };

            List<Model> AllModels = new List<Model>();
            AllModels.Add(Pizza);
            AllModels.Add(Salami);
            AllModels.Add(Cheese);
            AllModels.Add(CheeseOrigin);
            AllModels.Add(CheeseShape);

            Pizza.ParentModel = null;
            Salami.ParentModel = Pizza;
            Cheese.ParentModel = Pizza;
            CheeseOrigin.ParentModel = Cheese;
            CheeseShape.ParentModel = Cheese;

            _dbContext.Add(TestProject);
            _dbContext.Add(Pizza);
            _dbContext.Add(Salami);
            _dbContext.Add(SalamiMeat);
            _dbContext.Add(Cheese);
            _dbContext.SaveChanges();
        }

        public string RetrieveModels(string model)
        {
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Error = (sender, args) =>
                {
                    args.ErrorContext.Handled = true;
                },
            };

            //var project = _dbContext.Projects.Include(p => p.Models).ThenInclude(m => m.Properties);
            //return JsonConvert.SerializeObject(project, settings);

            var pizza = _dbContext.Models.Where(m => m.Name.Equals(model)).First();
            LoadRecursively(pizza);

            return JsonConvert.SerializeObject(pizza, settings);
        }

        public string CreateModel([FromBody] CreateModelData modelData)
        {
            var ToAddProject = _dbContext.Projects.Where(p => p.ID == modelData.ProjectId).First();

            Model NewModel = new Model()
            {
                Name = modelData.ModelName,
                ProjectId = ToAddProject
            };

            //if (ToAddProject != null)
            //{
            //    ToAddProject.Models.Add(NewModel);
            //}
            _dbContext.Models.Add(NewModel);
            _dbContext.SaveChanges();

            return JsonConvert.SerializeObject(NewModel);
        }

        public void LoadRecursively(Model model)
        {
            _dbContext.Entry(model).Collection(m => m.SubModels).Load();
            _dbContext.Entry(model).Collection(m => m.Properties).Load();
            _dbContext.Entry(model).Reference(m => m.ProjectId).Load();
            //_dbContext.Entry(model).Reference(m => m.ParentModel).Load();

            foreach (var SubModel in model.SubModels)
            {
                _dbContext.Entry(SubModel).Collection(m => m.Properties).Load();
                _dbContext.Entry(model).Reference(m => m.ProjectId).Load();
                //_dbContext.Entry(model).Reference(m => m.ParentModel).Load();
                LoadRecursively(SubModel);
            }
        }

        public void BulkInsert()
        {
            List<Model> insertList = new List<Model>();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            for (int i = 0; i < 1000000; i++)
            {

                string randomString = new string(Enumerable.Repeat(chars, 5)
                    .Select(s => s[random.Next(s.Length)]).ToArray());

                Model temp = new Model
                {
                    Name = randomString,
                };
                insertList.Add(temp);
            }

            _dbContext.Models.BulkInsert(insertList);
        }

        //[HttpGet]
        //[EnableQuery]
        //public IEnumerable<Model> Get()
        //{
        //    return _dbContext.Models;//.Where(m => m.ID == key).Include(m => m.SubModel);
        //}

    }
}

