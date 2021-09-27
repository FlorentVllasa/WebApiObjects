using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiObjects.Models
{
    public class ModelType
    {
        public Guid ID { get; set; }
        public List<Type> ModelTypes { get; set; }
        public List<Model> Models { get; set; }

    }
}
