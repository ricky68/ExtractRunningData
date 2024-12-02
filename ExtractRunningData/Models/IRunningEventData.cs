namespace ExtractRunningData.Models
{
    /// <summary>
    /// Interface for RunningEventFactors
    /// </summary>
    public interface IRunningEventFactors
    {
        int Age { get; set; }
        RunningEventData RunningEventData { get; set; }
        int RunningEventDataId { get; set; }
        int RunningEventFactorsId { get; set; }
        double Factor { get; set; }
    }

    /// <summary>
    /// Interface for RunningEventData
    /// </summary>
    public interface IRunningEventData
    {
        double Distance { get; set; }
        int RunningEventDataId { get; set; }
        ICollection<RunningEventFactors> RunningEventFactors { get; set; }
        string EventName { get; set; }
        bool IsRoad { get; set; }
        double OC { get; set; }
        char Sex { get; set; }
    }
}