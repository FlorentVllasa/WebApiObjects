using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebApiObjects.Models
{
    public class Model
    {
        [Key]
        public Guid ID { get; set; }

        public string Name { get; set; }

        public List<Model> SubModels { get; set; }

        [JsonIgnore]
        public Model ParentModel { get; set; }

        public Guid? ParentId { get; set; }

        public Project ProjectId { get; set; }

        public List<Property> Properties { get; set; }

    }
}
