using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiObjects.Models
{
    public class Project
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        //public ICollection<Model> Models { get; set; }
    }
}
