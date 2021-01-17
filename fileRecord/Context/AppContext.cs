using System;
using fileRecord.Models;
using Microsoft.EntityFrameworkCore;

namespace fileRecord.Context
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> opt) : base(opt)
        {
        }

        public DbSet<FileModel> Files { get; set; }
    }
}
