using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ADSBackend.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Tag Name")]
        public string TagName { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
