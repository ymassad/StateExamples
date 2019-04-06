using System;
using System.Diagnostics;
using System.Threading;

namespace MultithreadingAndRefStateParametersAndLockStatement
{
    public static class GermanTextTranslationModule
    {
        public static Text TranslateFromGerman(
            Text text,
            Location location,
            ref ServerCommunicationStatistics statisticsState,
            object statisticsStateLockingObject)
        {
            bool useServer1 = DateTime.Now.Millisecond < 500;

            if (useServer1)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();

                var result = TranslateFromGermanViaServer1(text, location);

                var elapsed = stopwatch.Elapsed;

                lock (statisticsStateLockingObject)
                {
                    statisticsState = statisticsState
                        .WithTotalTimeSpentCommunicatingWithServer1(
                            statisticsState.TotalTimeSpentCommunicatingWithServer1 + elapsed)
                        .WithNumberOfTimesCommunicatedWithServer1(
                            statisticsState.NumberOfTimesCommunicatedWithServer1 + 1);
                }

                return result;
            }

            Stopwatch stopwatch2 = Stopwatch.StartNew();

            var resultFromServer2 = TranslateFromGermanViaServer2(text, location);

            var elapsed2 = stopwatch2.Elapsed;

            lock (statisticsStateLockingObject)
            {
                statisticsState = statisticsState
                    .WithTotalTimeSpentCommunicatingWithServer2(
                        statisticsState.TotalTimeSpentCommunicatingWithServer2 + elapsed2)
                    .WithNumberOfTimesCommunicatedWithServer2(
                        statisticsState.NumberOfTimesCommunicatedWithServer2 + 1);
            }

            return resultFromServer2;
        }

        private static Text TranslateFromGermanViaServer1(Text text, Location location)
        {
            Thread.Sleep(5);

            return new Text(text.Value.Replace("tag", "day") + "_1_" + location);
        }

        private static Text TranslateFromGermanViaServer2(Text text, Location location)
        {
            Thread.Sleep(5);

            return new Text(text.Value.Replace("tag", "day") + "_2_" + location);
        }
    }
}