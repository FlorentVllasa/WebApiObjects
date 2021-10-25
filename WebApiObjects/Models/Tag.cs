using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiObjects.Models
{
    public class Tag
    {
        public Guid ID { get; set; }
        public List<string> Tags { get; set; }
        public Model ParentModel { get; set; }
        public Guid ParentModelID { get; set; }

    }
}
