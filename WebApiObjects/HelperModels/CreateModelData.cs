using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiObjects.Models;

namespace WebApiObjects.HelperModels
{
    public class CreateModelData
    {
        public int ProjectId { get; set; }
        public string ModelName { get; set; }

        public Model ParentModel { get; set; }

    }
}
