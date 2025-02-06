using Microsoft.VisualBasic;
using System.Data;

namespace TTRPGForum.Models
{
    public class Comment
    {
        //primary key
        public int CommentId { get; set; }

        public string Content { get; set; } = string.Empty;

        public DateTime  CreateDate { get; set; }


        //foreign key
        public int DiscussionId { get; set; }

        //Navigation Property
        public Discussion? Discussion { get; set; }
    }
}
