using ExtractRunningData.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace ExtractRunningData
{
    /// <summary>
    /// EventDataContext class
    /// </summary>
    /// <remarks>
    /// Constructor
    /// </remarks>
    /// <param name="options"></param>
    public class EventDataContext(DbContextOptions<EventDataContext> options) : DbContext(options), IEventDataContext
    {
        /// <summary>
        /// RunningEventData DbSet
        /// </summary>
        public DbSet<RunningEventData> RunningEventData { get; set; } = default!;
    }
}