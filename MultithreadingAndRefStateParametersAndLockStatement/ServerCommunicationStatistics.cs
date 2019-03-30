using System;

namespace MultithreadingAndRefStateParametersAndLockStatement
{
    public sealed class ServerCommunicationStatistics
    {
        public ServerCommunicationStatistics(int numberOfTimesCommunicatedWithServer1, int numberOfTimesCommunicatedWithServer2, TimeSpan totalTimeSpentCommunicatingWithServer1, TimeSpan totalTimeSpentCommunicatingWithServer2)
        {
            NumberOfTimesCommunicatedWithServer1 = numberOfTimesCommunicatedWithServer1;
            NumberOfTimesCommunicatedWithServer2 = numberOfTimesCommunicatedWithServer2;
            TotalTimeSpentCommunicatingWithServer1 = totalTimeSpentCommunicatingWithServer1;
            TotalTimeSpentCommunicatingWithServer2 = totalTimeSpentCommunicatingWithServer2;
        }

        public int NumberOfTimesCommunicatedWithServer1 { get; }

        public int NumberOfTimesCommunicatedWithServer2 { get; }

        public TimeSpan TotalTimeSpentCommunicatingWithServer1 { get; }

        public TimeSpan TotalTimeSpentCommunicatingWithServer2 { get; }
    }
}