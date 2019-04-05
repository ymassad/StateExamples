using System;

namespace MultithreadingAndRefStateParametersAndStateHolder
{
    [CreateWithMethods(typeof(ServerCommunicationStatistics))]
    public static class ServerCommunicationStatisticsExtensionMethods
    {
        public static ServerCommunicationStatistics WithNumberOfTimesCommunicatedWithServer1(this ServerCommunicationStatistics instance, Int32 newValue)
        {
            return new ServerCommunicationStatistics(numberOfTimesCommunicatedWithServer1: newValue, numberOfTimesCommunicatedWithServer2: instance.NumberOfTimesCommunicatedWithServer2, totalTimeSpentCommunicatingWithServer1: instance.TotalTimeSpentCommunicatingWithServer1, totalTimeSpentCommunicatingWithServer2: instance.TotalTimeSpentCommunicatingWithServer2);
        }

        public static ServerCommunicationStatistics WithNumberOfTimesCommunicatedWithServer2(this ServerCommunicationStatistics instance, Int32 newValue)
        {
            return new ServerCommunicationStatistics(numberOfTimesCommunicatedWithServer1: instance.NumberOfTimesCommunicatedWithServer1, numberOfTimesCommunicatedWithServer2: newValue, totalTimeSpentCommunicatingWithServer1: instance.TotalTimeSpentCommunicatingWithServer1, totalTimeSpentCommunicatingWithServer2: instance.TotalTimeSpentCommunicatingWithServer2);
        }

        public static ServerCommunicationStatistics WithTotalTimeSpentCommunicatingWithServer1(this ServerCommunicationStatistics instance, TimeSpan newValue)
        {
            return new ServerCommunicationStatistics(numberOfTimesCommunicatedWithServer1: instance.NumberOfTimesCommunicatedWithServer1, numberOfTimesCommunicatedWithServer2: instance.NumberOfTimesCommunicatedWithServer2, totalTimeSpentCommunicatingWithServer1: newValue, totalTimeSpentCommunicatingWithServer2: instance.TotalTimeSpentCommunicatingWithServer2);
        }

        public static ServerCommunicationStatistics WithTotalTimeSpentCommunicatingWithServer2(this ServerCommunicationStatistics instance, TimeSpan newValue)
        {
            return new ServerCommunicationStatistics(numberOfTimesCommunicatedWithServer1: instance.NumberOfTimesCommunicatedWithServer1, numberOfTimesCommunicatedWithServer2: instance.NumberOfTimesCommunicatedWithServer2, totalTimeSpentCommunicatingWithServer1: instance.TotalTimeSpentCommunicatingWithServer1, totalTimeSpentCommunicatingWithServer2: newValue);
        }
    }
}