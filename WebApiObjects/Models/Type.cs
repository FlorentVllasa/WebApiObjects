using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiObjects.Models
{
    public class Type
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Typ { get; set; }
        public ModelType ParentModelType { get; set; }
        public Guid ParentModelTypeId { get; set; }

    }
}
