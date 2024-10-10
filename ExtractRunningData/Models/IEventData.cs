namespace ExtractRunningData.Models
{
    public interface IEventFactors
    {
        int Age { get; set; }
        EventData EventData { get; set; }
        int EventDataId { get; set; }
        int EventFactorsId { get; set; }
        double Factor { get; set; }
    }

    public interface IEventData
    {
        double Distance { get; set; }
        int EventDataId { get; set; }
        ICollection<EventFactors> EventFactors { get; set; }
        string EventName { get; set; }
        bool IsRoad { get; set; }
        double OC { get; set; }
        char Sex { get; set; }
    }
}