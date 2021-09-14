﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiObjects.Models
{
    public class Action
    {
        [Key]
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Method { get; set; }

        public Model ParentModel { get; set; }

        public Guid? ParentId { get; set; }

    }
}
