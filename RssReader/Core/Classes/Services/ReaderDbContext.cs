using Microsoft.EntityFrameworkCore;
using RssReader.Core.Classes.Entities;

namespace RssReader.Core.Classes.Services
{
    public class ReaderDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Feed> Feeds { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=DbRssReader.sqlite");
        }
    }
}
