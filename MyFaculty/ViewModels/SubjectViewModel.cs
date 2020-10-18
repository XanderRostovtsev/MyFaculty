using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFaculty.ViewModels
{
    public class SubjectViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int GroupId { get; set; }

        public string GroupTitle { get; set; }

        [Required]
        public int TeacherId { get; set; }

        public string TeacherLFM { get; set; }

        [Required]
        public int SemesterId { get; set; }

        public int SemesterCount { get; set; }

        public List<MarkViewModel> Marks { get; set; }
    }
}
