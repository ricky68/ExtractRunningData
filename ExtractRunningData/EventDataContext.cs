using ExtractRunningData.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace ExtractRunningData
{
    //public class EventDataContext : DbContext, IEventDataContext, IDisposable
    //{
    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        optionsBuilder.UseMySql("server=localhost;port=3306;database=Events;user=root;password=panther",
    //            new MySqlServerVersion(new Version(11, 5, 2))); // Update this line
    //    }

    //    public DbSet<EventData> EventData { get; set; }
    //}
    public class EventDataContext : DbContext, IEventDataContext, IDisposable
    {
        private readonly DbContextOptions<EventDataContext> _options;

        public EventDataContext(DbContextOptions<EventDataContext> options)
            : base(options)
        {
            _options = options;
        }

        public DbSet<EventData> EventData { get; set; }
    }
}