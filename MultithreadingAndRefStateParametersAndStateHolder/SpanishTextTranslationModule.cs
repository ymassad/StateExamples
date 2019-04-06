using System;
using System.Diagnostics;
using System.Threading;

namespace MultithreadingAndRefStateParametersAndStateHolder
{
    public static class SpanishTextTranslationModule
    {
        public static Text TranslateFromSpanish(
            Text text,
            Location location,
            IStateUpdater<ServerCommunicationStatistics> statisticsStateUpdater)
        {
            bool useServer1 = DateTime.Now.Millisecond < 500;

            if (useServer1)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();

                var result = TranslateFromSpanishViaServer1(text, location);

                var elapsed = stopwatch.Elapsed;

                statisticsStateUpdater.UpdateState(statisticsState =>
                    statisticsState
                        .WithTotalTimeSpentCommunicatingWithServer1(
                            statisticsState.TotalTimeSpentCommunicatingWithServer1 + elapsed)
                        .WithNumberOfTimesCommunicatedWithServer1(
                            statisticsState.NumberOfTimesCommunicatedWithServer1 + 1));

                return result;
            }

            Stopwatch stopwatch2 = Stopwatch.StartNew();

            var resultFromServer2 = TranslateFromSpanishViaServer2(text, location);

            var elapsed2 = stopwatch2.Elapsed;

            statisticsStateUpdater.UpdateState(statisticsState =>
                statisticsState
                    .WithTotalTimeSpentCommunicatingWithServer2(
                        statisticsState.TotalTimeSpentCommunicatingWithServer2 + elapsed2)
                    .WithNumberOfTimesCommunicatedWithServer2(
                        statisticsState.NumberOfTimesCommunicatedWithServer2 + 1));

            return resultFromServer2;
        }

        private static Text TranslateFromSpanishViaServer1(Text text, Location location)
        {
            Thread.Sleep(5);

            return new Text(text.Value.Replace("hola", "hello") + "_1_" + location);
        }

        private static Text TranslateFromSpanishViaServer2(Text text, Location location)
        {
            Thread.Sleep(5);

            return new Text(text.Value.Replace("hola", "hello") + "_2_" + location);
        }
    }
}