using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractRunningData.Models
{
    /// <summary>
    /// create initialiser for event data
    /// </summary>
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

        /// <summary>
        /// Default constructor
        /// </summary>
        public RunningEventData() {
            RunningEventFactors = [];
        }

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="eventDataId"></param>
        /// <param name="eventName"></param>
        /// <param name="isRoad"></param>
        /// <param name="distance"></param>
        /// <param name="oc"></param>
        /// <param name="sex"></param>
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

    /// <summary>
    /// RunningEventFactors class
    /// </summary>
    public class RunningEventFactors : IRunningEventFactors
    {
        public int RunningEventFactorsId { get; set; } // Auto-incremented index
        public int Age { get; set; }
        public double Factor { get; set; }

        // Foreign key referencing EventData.Id
        public int RunningEventDataId { get; set; }

        // Navigation property to link to the EventData
        public required RunningEventData RunningEventData { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public RunningEventFactors() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventFactorsId"></param>
        /// <param name="age"></param>
        /// <param name="factor"></param>
        public RunningEventFactors(int eventFactorsId, int age, int factor)
        {
            RunningEventFactorsId = eventFactorsId;
            Age = age;
            Factor = factor;
        }
    }
}
