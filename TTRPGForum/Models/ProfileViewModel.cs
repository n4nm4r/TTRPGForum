using System.Collections.Generic;
using TTRPGForum.Data;

namespace TTRPGForum.Models
{
    public class ProfileViewModel
    {
        public ApplicationUser User { get; set; }
        public List<Discussion> Discussions { get; set; }
    }
}
