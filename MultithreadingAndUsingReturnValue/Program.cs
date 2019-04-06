using System;

namespace MultithreadingAndUsingReturnValue
{
    class Program
    {
        static void Main(string[] args)
        {
            var serverCommunicationStatisticsState =
                FolderProcessingModule.TranslateDocumentsInFolderInParallel(
                    "c:\\inputFolder1",
                    "c:\\outputFolder1",
                    Location.A,
                    ServerCommunicationStatistics.Zero());

            Console.WriteLine(
                "Number of times communicated with server 1: "
                + serverCommunicationStatisticsState.NumberOfTimesCommunicatedWithServer1);

            Console.WriteLine(
                "Total time spent communicating with server 1: "
                + serverCommunicationStatisticsState.TotalTimeSpentCommunicatingWithServer1);

            Console.WriteLine(
                "Number of times communicated with server 2: "
                + serverCommunicationStatisticsState.NumberOfTimesCommunicatedWithServer2);

            Console.WriteLine(
                "Total time spent communicating with server 2: "
                + serverCommunicationStatisticsState.TotalTimeSpentCommunicatingWithServer2);
        }
    }
}
