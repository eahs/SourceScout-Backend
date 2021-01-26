using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADSBackend.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public string Title { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public int UpVotes { get; set; } = 1;
        public int DownVotes { get; set; }
        public double Score { get; set; }
        public string Thumbnail { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        [DisplayName("Date Created")]
        public DateTime DateCreated { get; set; }
        [DisplayName("Date Edited")]
        public DateTime DateEdited { get; set; }
        public bool Deleted { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<PostTag> Tags { get; set; }

        [NotMapped]
        public List<String> TagNames { get; set; }
    }
}
