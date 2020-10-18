using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFaculty.ViewModels
{
    public class SemesterViewModel
    {
        public int Id { get; set;  }
        
        [Required]
        public int Count { get; set; }

        [Required]
        public int StartYear { get; set; }

        [Required]
        public int EndYear { get; set; }
    }
}
