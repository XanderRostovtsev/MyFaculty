using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFaculty.Data.Context.Entity
{
    public class SemesterEntity
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
    }
}
