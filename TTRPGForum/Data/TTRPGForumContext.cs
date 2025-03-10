using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TTRPGForum.Models;

namespace TTRPGForum.Data
{
    //Use custom application user
    public class TTRPGForumContext : IdentityDbContext<ApplicationUser>
    {
        public TTRPGForumContext (DbContextOptions<TTRPGForumContext> options)
            : base(options)
        {
        }

        public DbSet<TTRPGForum.Models.Discussion> Discussion { get; set; } = default!;
        public DbSet<TTRPGForum.Models.Comment> Comment { get; set; } = default!;
    }
}
