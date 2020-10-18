using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFaculty.ViewModels
{
    public class MarkViewModel
    {
        public int Id { get; set; }
        public int Mark { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public string Date { get; set; }
    }
}
