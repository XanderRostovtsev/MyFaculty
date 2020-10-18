using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFaculty.Data.Context.Entity
{
    public class SubjectEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int GroupId { get; set; }
        public int TeacherId { get; set; }
        public int SemesterId { get; set; }
    }
}
