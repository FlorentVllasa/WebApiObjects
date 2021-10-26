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
            Models.Action SomeAction = new Models.Action
            {
                Method = "GET",
                Name = "Get Something",
                Url = "some/service",
            };

            Models.Action SomeAction2 = new Models.Action
            {
                Method = "GET",
                Name = "Get Something",
                Url = "some/service",
            };

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
            List<Models.Action> CheesePizzaList = new List<Models.Action>();
            List<string> TagStrings = new List<string>();
            TagStrings.Add("PizzaTag");

            Tag tags = new Tag
            {
                Tags = TagStrings,
            };

            List<Tag> tagList = new List<Tag>();
            tagList.Add(tags);

            CheesePizzaList.Add(SomeAction2);
            Model Cheese = new Model
            {
                text = "cheese",
                ProjectId = TestProject,
                Properties = CheeseProperties,
                Actions = CheesePizzaList,
                Tags = tagList
            };
            SomeAction2.ParentModel = Cheese;
            tags.ParentModel = Cheese;

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

            List<Models.Action> ActionPizzaList = new List<Models.Action>();
            
            ActionPizzaList.Add(SomeAction);

            Model Pizza = new Model
            {
                text = "pizza",
                children = Ingredients,
                ProjectId = TestProject,
                Actions = ActionPizzaList
            };
            SomeAction.ParentModel = Pizza;

            List<Model> AllModels = new List<Model>();
            AllModels.Add(Pizza);
            AllModels.Add(Salami);
            AllModels.Add(Cheese);

            Pizza.ParentModel = null;
            Salami.ParentModel = Pizza;
            Cheese.ParentModel = Pizza;

            List<Models.Type> TypeList = new List<Models.Type>();
           
            ModelType ModelType = new ModelType
            {
                Models = AllModels,
                DataVariables = TypeList
            };

            Models.Type PizzaType = new Models.Type
            {
                Name = "SalamiPizza",
                Typ = "pizza",
                ParentModelType = ModelType

            };

            Models.Type CheeseType = new Models.Type
            {
                Name = "Cheese Variety",
                Typ = "cheese[]",
                ParentModelType = ModelType
            };

            TypeList.Add(PizzaType);
            TypeList.Add(CheeseType);

            _dbContext.Add(TestProject);
            _dbContext.Add(Pizza);
            _dbContext.Add(Salami);
            _dbContext.Add(SalamiMeat);
            _dbContext.Add(Cheese);
            _dbContext.Add(Shape);
            _dbContext.Add(Origin);
            _dbContext.Add(PizzaType);
            _dbContext.Add(CheeseType);
            _dbContext.Add(ModelType);
            _dbContext.Add(SomeAction);
            _dbContext.Add(SomeAction2);
            _dbContext.Add(tags);
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

            var ToAddProject = _dbContext.Projects.Where(p => p.ID == modelData.ProjectId).First();

            if (modelData.ParentModel == null)
            { 

                Model NewModel = new Model()
                {
                    text = modelData.ModelName,
                    ProjectId = ToAddProject,
                    ParentModel = null
                };

                _dbContext.Models.Add(NewModel);
                _dbContext.SaveChanges();

                return JsonConvert.SerializeObject(NewModel);
            }

            Model NewModelWithParent = new Model()
            {
                text = modelData.ModelName,
                ProjectId = ToAddProject,
                ParentModel = modelData.ParentModel
            };

            _dbContext.Models.Add(NewModelWithParent);
            _dbContext.SaveChanges();

            return JsonConvert.SerializeObject(modelData.ParentModel);
        }

        public void LoadRecursively(Model model)
        {
            _dbContext.Entry(model).Collection(m => m.children).Load();
            _dbContext.Entry(model).Collection(m => m.Properties).Load();
            _dbContext.Entry(model).Reference(m => m.ProjectId).Load();
            _dbContext.Entry(model).Collection(m => m.Actions).Load();
            _dbContext.Entry(model).Collection(m => m.Tags).Load();
            //_dbContext.Entry(model).Reference(m => m.ParentModel).Load();

            foreach (var SubModel in model.children)
            {
                _dbContext.Entry(SubModel).Collection(m => m.Properties).Load();
                _dbContext.Entry(model).Reference(m => m.ProjectId).Load();
                _dbContext.Entry(model).Collection(m => m.Actions).Load();
                _dbContext.Entry(model).Collection(m => m.Tags).Load();
                //_dbContext.Entry(model).Reference(m => m.ParentModel).Load();
                LoadRecursively(SubModel);
            }
        }

        public void LoadRecursivelyTypes(ModelType ModelType)
        {
            _dbContext.Entry(ModelType).Collection(mt => mt.DataVariables).Load();
            _dbContext.Entry(ModelType).Collection(mt => mt.Models).Load();

            foreach (var Model in ModelType.Models)
            {
                foreach (var SubModel in Model.children)
                {
                    LoadRecursively(SubModel);
                }
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

