using ExtractRunningData.Models;

namespace ExtractRunningDataTests
{
    internal class TestData
    {
        public static RunningEventData First()
        {
            var eventData = new RunningEventData
            {
                RunningEventDataId = 1,
                EventName = "50Hur",
                IsRoad = false,
                Distance = 0,
                OC = 6.58,
                Sex = 'F',
            };

            // edge cases
            eventData.RunningEventFactors =
            [
                new() {
                    RunningEventFactorsId = 1,
                    Age = 5,
                    Factor = 0,
                    RunningEventData = eventData
                }
            ,
                new() {
                    RunningEventFactorsId = 96,
                    Age = 100,
                    Factor = 0.2417,
                    RunningEventData = eventData
                }
            ];

            return eventData;
        }

        public static RunningEventData Last()
        {
            var eventData = new RunningEventData
            {
                RunningEventDataId = 73,
                EventName = "200km",
                IsRoad = true,
                Distance = 200,
                OC = 52800,
                Sex = 'M',
            };

            // edge cases
            eventData.RunningEventFactors =
            [
                new() {
                    RunningEventFactorsId = 1173,
                    Age = 5,
                    Factor = 0.6056,
                    RunningEventData = eventData
                }
            ,
                new() {
                    RunningEventFactorsId = 1196,
                    Age = 100,
                    Factor = 0.1921,
                    RunningEventData = eventData
                }
            ];

            return eventData;
        }
    }
}

