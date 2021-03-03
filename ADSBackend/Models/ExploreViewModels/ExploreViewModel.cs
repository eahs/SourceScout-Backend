using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADSBackend.Data;
using ADSBackend.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace ADSBackend.Models.ExploreViewModels
{
    public class ExploreViewModel
    {

        public ExploreViewModel(ApplicationDbContext context)
        {
            
        }
        public string SearchInput { get; set; }
        public ApplicationUser User { get; set; }

        public List<Post> queryResult { get; set; }
    }
}

