using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractRunningData.Models
{
    // create initialiser for event data
    public class RunningEventData : IRunningEventData
    {
        public int RunningEventDataId { get; set; } // Auto-incremented index
        public required string EventName { get; set; }
        public bool IsRoad { get; set; }
        public double Distance { get; set; }
        public double OC { get; set; }
        public char Sex { get; set; }
        // Navigation property to link to associated EventFactors
        public ICollection<RunningEventFactors> RunningEventFactors { get; set; } = [];

        public RunningEventData() { }

        public RunningEventData(int eventDataId, string eventName, bool isRoad, double distance, double oc, char sex)
        {
            RunningEventDataId = eventDataId;
            EventName = eventName;
            IsRoad = isRoad;
            Distance = distance;
            OC = oc;
            Sex = sex;
        }
    }

    public class RunningEventFactors : IRunningEventFactors
    {
        public int RunningEventFactorsId { get; set; } // Auto-incremented index
        public int Age { get; set; }
        public double Factor { get; set; }

        // Foreign key referencing EventData.Id
        public int RunningEventDataId { get; set; }

        // Navigation property to link to the EventData
        public required RunningEventData RunningEventData { get; set; }

        public RunningEventFactors() { }

        public RunningEventFactors(int eventFactorsId, int age, int factor)
        {
            RunningEventFactorsId = eventFactorsId;
            Age = age;
            Factor = factor;
        }
    }
}
