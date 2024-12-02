using ExtractRunningData.Models;
using Microsoft.EntityFrameworkCore;

namespace ExtractRunningData
{
    /// <summary>
    /// EventDataContext interface
    /// </summary>
    public interface IEventDataContext
    {
        DbSet<RunningEventData> RunningEventData { get; set; }

        void Dispose();
    }
}