using ExtractRunningData.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace ExtractRunningData
{
    public class EventDataContext : DbContext, IEventDataContext, IDisposable
    {
        private readonly DbContextOptions<EventDataContext> _options;

        public EventDataContext(DbContextOptions<EventDataContext> options)
            : base(options)
        {
            _options = options;
        }

        public DbSet<RunningEventData> RunningEventData { get; set; }
    }
}