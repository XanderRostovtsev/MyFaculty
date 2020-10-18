using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFaculty.ViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }

        [Required]
        public int GroupId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public List<SubjectViewModel> Subjects { get; set; }

        public List<MarkViewModel> Marks { get; set; }
    }
}
