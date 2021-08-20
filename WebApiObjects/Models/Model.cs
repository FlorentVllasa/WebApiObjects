using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebApiObjects.Models
{
    public class Model
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Model> SubModel { get; set; }
        public ICollection<Property> Properties { get; set; }

    }
}
