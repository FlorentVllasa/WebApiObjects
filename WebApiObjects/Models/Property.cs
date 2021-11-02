using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiObjects.Models
{
    public class Property
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        [JsonIgnore]
        public string Value { get; set; }
        [JsonIgnore]
        public Model ParentModel { get; set; }
        public Guid ParentId { get; set; }

    }
}
