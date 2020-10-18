using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFaculty.ViewModels
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }

        public List<StudentViewModel> Students { get; set; }
    }
}
