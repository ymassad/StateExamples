using System;
using System.Diagnostics;
using System.Threading;

namespace MultithreadingAndRefStateParametersAndComplexCAS
{
    public static class SpanishTextTranslationModule
    {
        public static Text TranslateFromSpanish(
            Text text,
            Location location,
            ref Server1State server1State,
            ref ServerCommunicationStatistics statisticsState)
        {
            bool useServer1 = true;

            if (server1State.Server1IsDown)
            {
                if (DateTime.Now - server1State.Server1DownSince < TimeSpan.FromMinutes(10))
                {
                    useServer1 = false;
                }
            }

            if (useServer1)
            {
                try
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();

                    var result = TranslateFromSpanishViaServer1(text, location);

                    var elapsed = stopwatch.Elapsed;

                    Utilities.UpdateViaCAS(ref statisticsState, state =>
                        state
                            .WithTotalTimeSpentCommunicatingWithServer1(
                                state.TotalTimeSpentCommunicatingWithServer1 + elapsed)
                            .WithNumberOfTimesCommunicatedWithServer1(
                                state.NumberOfTimesCommunicatedWithServer1 + 1));

                    server1State = new Server1State(false, DateTime.MinValue);

                    return result;
                }
                catch
                {
                    server1State = new Server1State(true, DateTime.Now);
                }
            }

            Stopwatch stopwatch2 = Stopwatch.StartNew();

            var resultFromServer2 = TranslateFromSpanishViaServer2(text, location);

            var elapsed2 = stopwatch2.Elapsed;

            Utilities.UpdateViaCAS(ref statisticsState, state =>
                state
                    .WithTotalTimeSpentCommunicatingWithServer2(
                        state.TotalTimeSpentCommunicatingWithServer2 + elapsed2)
                    .WithNumberOfTimesCommunicatedWithServer2(
                        state.NumberOfTimesCommunicatedWithServer2 + 1));

            return resultFromServer2;
        }

        private static Text TranslateFromSpanishViaServer1(Text text, Location location)
        {
            if(DateTime.Now.Millisecond < 500)
                throw new Exception("Error");

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