using Microsoft.AspNetCore.OData.Routing.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiObjects.Models;

namespace WebApiObjects.Controllers
{
    public class ModelTypesController : ODataController
    {
        WebDbContext _dbContext;

        public ModelTypesController(WebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string RetrieveType(Guid type)
        {
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Error = (sender, args) =>
                {
                    args.ErrorContext.Handled = true;
                },
            };


            var ToSearchModelType = _dbContext.ModelTypes.Where(mt => mt.ID == type).First();
            LoadRecursivelyTypes(ToSearchModelType);
            return JsonConvert.SerializeObject(ToSearchModelType, settings);
        }

        public void LoadRecursivelyTypes(ModelType ModelType)
        {
            _dbContext.Entry(ModelType).Collection(mt => mt.DataVariables).Load();
            _dbContext.Entry(ModelType).Collection(mt => mt.Models).Load();

            foreach (var Model in ModelType.Models)
            {
                foreach (var SubModel in Model.SubModels)
                {
                    LoadRecursively(SubModel);
                }
            }
        }
        public void LoadRecursively(Model model)
        {
            _dbContext.Entry(model).Collection(m => m.SubModels).Load();
            _dbContext.Entry(model).Collection(m => m.Properties).Load();
            _dbContext.Entry(model).Reference(m => m.ProjectId).Load();
            _dbContext.Entry(model).Collection(m => m.Actions).Load();
            _dbContext.Entry(model).Collection(m => m.Tags).Load();
            //_dbContext.Entry(model).Reference(m => m.ParentModel).Load();

            foreach (var SubModel in model.SubModels)
            {
                _dbContext.Entry(SubModel).Collection(m => m.Properties).Load();
                _dbContext.Entry(model).Reference(m => m.ProjectId).Load();
                _dbContext.Entry(model).Collection(m => m.Actions).Load();
                _dbContext.Entry(model).Collection(m => m.Tags).Load();
                //_dbContext.Entry(model).Reference(m => m.ParentModel).Load();
                LoadRecursively(SubModel);
            }
        }

    }
}
