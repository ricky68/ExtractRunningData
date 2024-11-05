using ExtractRunningData.Models;
using Microsoft.EntityFrameworkCore;

namespace ExtractRunningData
{
    public interface IEventDataContext
    {
        DbSet<RunningEventData> RunningEventData { get; set; }

        void Dispose();
    }
}