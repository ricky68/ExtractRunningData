using ExtractRunningData.Models;
using Microsoft.EntityFrameworkCore;

namespace ExtractRunningData
{
    public interface IEventDataContext
    {
        DbSet<EventData> EventData { get; set; }

        void Dispose();
    }
}