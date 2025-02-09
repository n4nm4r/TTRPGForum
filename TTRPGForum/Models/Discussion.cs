using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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

        //Proprty for file upload
        [NotMapped]
        [Display(Name = "File name")]
        public IFormFile? ImageFile { get; set; } // nullable!!!


        public DateTime CreateDate { get; set; }

        //Navigation Property
        public List<Comment>? Comments { get; set; }
    }
}
