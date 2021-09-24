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
                text = "Salami",
                children = null,
                Properties = properties,
                ProjectId = TestProject
            };

            Property SalamiMeat = new Property
            {
                Name = "Salami Meat",
                ParentModel = Salami,
                Type = "string"
            };

            properties.Add(SalamiMeat);

            List<Property> CheeseProperties = new List<Property>();
            
            Model Cheese = new Model
            {
                text = "Cheese",
                ProjectId = TestProject,
                Properties = CheeseProperties
            };

            Property Shape = new Property
            {
                Name = "Shape",
                ParentModel = Cheese,
                Type = "string"
            };

            Property Origin = new Property
            {
                Name = "Origin",
                ParentModel = Cheese,
                Type = "string"
            };

            CheeseProperties.Add(Shape);
            CheeseProperties.Add(Origin);

            List<Model> Ingredients = new List<Model>();
            Ingredients.Add(Salami);
            Ingredients.Add(Cheese);

            Model Pizza = new Model
            {
                text = "pizza",
                children = Ingredients,
                ProjectId = TestProject
            };

            List<Model> AllModels = new List<Model>();
            AllModels.Add(Pizza);
            AllModels.Add(Salami);
            AllModels.Add(Cheese);

            Pizza.ParentModel = null;
            Salami.ParentModel = Pizza;
            Cheese.ParentModel = Pizza;

            _dbContext.Add(TestProject);
            _dbContext.Add(Pizza);
            _dbContext.Add(Salami);
            _dbContext.Add(SalamiMeat);
            _dbContext.Add(Cheese);
            _dbContext.Add(Shape);
            _dbContext.Add(Origin);
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

            var ToSearchModel = _dbContext.Models.Where(m => m.text.Equals(model)).First();
            //var pizza = _dbContext.Models.Where(m => m.Name.Equals(model)).First();
            LoadRecursively(ToSearchModel);

            return JsonConvert.SerializeObject(ToSearchModel, settings);
        }

        public string CreateModel([FromBody] CreateModelData modelData)
        {

            if(modelData.ParentModel == null)
            {
                var ToAddProject = _dbContext.Projects.Where(p => p.ID == modelData.ProjectId).First();

                Model NewModel = new Model()
                {
                    text = modelData.ModelName,
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
            return "";
        }

        public void LoadRecursively(Model model)
        {
            _dbContext.Entry(model).Collection(m => m.children).Load();
            _dbContext.Entry(model).Collection(m => m.Properties).Load();
            _dbContext.Entry(model).Reference(m => m.ProjectId).Load();
            //_dbContext.Entry(model).Reference(m => m.ParentModel).Load();

            foreach (var SubModel in model.children)
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
                    text = randomString,
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

