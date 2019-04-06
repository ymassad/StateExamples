using System;

namespace MultithreadingAndRefStateParametersAndStateHolder
{
    class Program
    {
        static void Main(string[] args)
        {
            var serverCommunicationStatisticsStateHolder =
                new ThreadSafeStateHolder<ServerCommunicationStatistics>(
                    new ServerCommunicationStatistics(
                        numberOfTimesCommunicatedWithServer1: 0,
                        numberOfTimesCommunicatedWithServer2: 0,
                        totalTimeSpentCommunicatingWithServer1: TimeSpan.Zero,
                        totalTimeSpentCommunicatingWithServer2: TimeSpan.Zero));

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
                                serverCommunicationStatisticsStateHolder),
                            text => SpanishTextTranslationModule.TranslateFromSpanish(
                                text,
                                Location.A,
                                serverCommunicationStatisticsStateHolder)))));

            var finalState = serverCommunicationStatisticsStateHolder.GetState();

            Console.WriteLine(
                "Number of times communicated with server 1: "
                + finalState.NumberOfTimesCommunicatedWithServer1);

            Console.WriteLine(
                "Total time spent communicating with server 1: "
                + finalState.TotalTimeSpentCommunicatingWithServer1);

            Console.WriteLine(
                "Number of times communicated with server 2: "
                + finalState.NumberOfTimesCommunicatedWithServer2);

            Console.WriteLine(
                "Total time spent communicating with server 2: "
                + finalState.TotalTimeSpentCommunicatingWithServer2);
        }
    }
}
