using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApiObjects.Models;

namespace WebApiObjects.Controllers
{
    public class ProjectsController : ODataController
    {
        WebDbContext _dbContext;
        public ProjectsController(WebDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public string Get()
        {
            List<Project> AllProjects = _dbContext.Projects.ToList();
            return JsonConvert.SerializeObject(AllProjects);
        }


        [HttpPost]
        public string CreateProject([FromBody] string ProjectName)
        {
            Debug.WriteLine(ProjectName);

            Project NewProject = new Project
            {
                Name = ProjectName
            };
            _dbContext.Add(NewProject);
            _dbContext.SaveChanges();

            Debug.WriteLine(NewProject.ID);
            //Project newProject = _dbContext.Projects.Where(p => p.Name.Equals(NewProject.Name)).First();
            return JsonConvert.SerializeObject(NewProject);
        }

    }
}
