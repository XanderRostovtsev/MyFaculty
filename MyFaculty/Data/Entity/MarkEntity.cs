using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFaculty.Data.Context.Entity
{
    public class MarkEntity
    {
        public int Id { get; set; }
        public int? Mark { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public string Date { get; set;  }
    }
}
