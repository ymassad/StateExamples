using System;

namespace MultithreadingAndRefStateParametersAndLockStatement
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerCommunicationStatistics serverCommunicationStatisticsState = 
                new ServerCommunicationStatistics(
                    numberOfTimesCommunicatedWithServer1: 0,
                    numberOfTimesCommunicatedWithServer2: 0,
                    totalTimeSpentCommunicatingWithServer1: TimeSpan.Zero,
                    totalTimeSpentCommunicatingWithServer2: TimeSpan.Zero);

            object serverCommunicationStatisticsStateLockingObject = new object();

            FolderProcessingModule.TranslateDocumentsInFolderInParallel(
                "c:\\inputFolder1",
                "c:\\outputFolder1",
                document => DocumentTranslationModule.TranslateDocument(
                    document,
                    paragraph => DocumentTranslationModule.TranslateParagraph(
                        paragraph,
                        paragraphText => DocumentTranslationModule.TranslateText(
                            paragraphText,
                            text => GermanTextTranslationModule.TranslateFromGerman(
                                text,
                                Location.A,
                                ref serverCommunicationStatisticsState,
                                serverCommunicationStatisticsStateLockingObject),
                            text => SpanishTextTranslationModule.TranslateFromSpanish(
                                text,
                                Location.A,
                                ref serverCommunicationStatisticsState,
                                serverCommunicationStatisticsStateLockingObject)))));

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
