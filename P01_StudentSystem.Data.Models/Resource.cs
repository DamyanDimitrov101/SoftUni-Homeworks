using P01_StudentSystem.Data.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_StudentSystem.Data.Models
{
    public class Resource
    {
        //•	Resource:
        //o ResourceId
        //o Name(up to 50 characters, unicode)
        //o Url(not unicode)
        //o ResourceType(enum – can be Video, Presentation, Document or Other)
        //o CourseId

        public int ResourceId { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
        
        public ResourceType ResourceType { get; set; }
        
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
