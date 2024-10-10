using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractRunningData.Models
{
    // create initialiser for event data
    public class EventData : IEventData
    {
        public int EventDataId { get; set; } // Auto-incremented index
        public required string EventName { get; set; }
        public bool IsRoad { get; set; }
        public double Distance { get; set; }
        public double OC { get; set; }
        public char Sex { get; set; }
        // Navigation property to link to associated EventFactors
        public ICollection<EventFactors> EventFactors { get; set; } = [];

        public EventData() { }

        public EventData(int eventDataId, string eventName, bool isRoad, double distance, double oc, char sex)
        {
            EventDataId = eventDataId;
            EventName = eventName;
            IsRoad = isRoad;
            Distance = distance;
            OC = oc;
            Sex = sex;
        }
    }

    public class EventFactors : IEventFactors
    {
        public int EventFactorsId { get; set; } // Auto-incremented index
        public int Age { get; set; }
        public double Factor { get; set; }

        // Foreign key referencing EventData.Id
        public int EventDataId { get; set; }

        // Navigation property to link to the EventData
        public required EventData EventData { get; set; }

        public EventFactors() { }

        public EventFactors(int eventFactorsId, int age, int factor)
        {
            EventFactorsId = eventFactorsId;
            Age = age;
            Factor = factor;
        }
    }
}
