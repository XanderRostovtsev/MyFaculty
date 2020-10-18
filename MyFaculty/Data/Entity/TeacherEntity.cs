using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFaculty.Data.Context.Entity
{
    public class TeacherEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Grade { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}
