using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TTRPGForum.Models;

namespace TTRPGForum.Data
{
    public class TTRPGForumContext : DbContext
    {
        public TTRPGForumContext (DbContextOptions<TTRPGForumContext> options)
            : base(options)
        {
        }

        public DbSet<TTRPGForum.Models.Discussion> Discussion { get; set; } = default!;
        public DbSet<TTRPGForum.Models.Comment> Comment { get; set; } = default!;
    }
}
