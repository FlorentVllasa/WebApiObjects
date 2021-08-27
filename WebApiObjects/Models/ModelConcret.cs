using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiObjects.Models
{
    public class ModelConcret
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Model> SubModels { get; set; }
        public List<Property> Properties { get; set; }

    }
}
