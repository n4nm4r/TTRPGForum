using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TTRPGForum.Data;

namespace TTRPGForum.Models
{
    public class Discussion
    {
        //primary key
        public int DiscussionId { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; } = string.Empty;

        public string Content {  get; set; } = string.Empty;


        
        public string ImageFilename {  get; set; } = string.Empty;

        //Property for file upload
        [NotMapped]
        [Display(Name = "File name")]
        public IFormFile? ImageFile { get; set; } // nullable!!!


        public DateTime CreateDate { get; set; }


        // Foreign key (AspNetUsers table)
        public string ApplicationUserId { get; set; } = string.Empty;
        // Navigation property
        public ApplicationUser? ApplicationUser { get; set; } // nullable!!!

        //Navigation Property
        public List<Comment>? Comments { get; set; }
    }
}
