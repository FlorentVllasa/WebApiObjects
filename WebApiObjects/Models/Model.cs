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

        public string text { get; set; }

        public List<Model> children { get; set; }

        [JsonIgnore]
        public Model ParentModel { get; set; }

        public Guid? ParentId { get; set; }

        public Project ProjectId { get; set; }

        public List<Property> Properties { get; set; }

        public List<Action> Actions { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
