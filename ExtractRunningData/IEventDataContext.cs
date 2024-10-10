using ExtractRunningData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;

namespace ExtractRunningData
{
    public interface IEventDataContext
    {
        DbSet<EventData> EventData { get; set; }

        void Dispose();
    }
}