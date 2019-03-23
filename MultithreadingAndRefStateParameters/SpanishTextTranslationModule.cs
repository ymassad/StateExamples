using System;

namespace MultithreadingAndRefStateParameters
{
    public static class SpanishTextTranslationModule
    {
        public static Text TranslateFromSpanish(
            Text text,
            Location location,
            ref Server1State server1State,
            ref int numberOfTimesCommunicatedWithServersState)
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
                    var result = TranslateFromSpanishViaServer1(text, location);

                    numberOfTimesCommunicatedWithServersState++;

                    server1State = new Server1State(false, DateTime.MinValue);

                    return result;
                }
                catch
                {
                    server1State = new Server1State(true, DateTime.Now);
                }
            }

            var resultFromServer2 = TranslateFromSpanishViaServer2(text, location);

            numberOfTimesCommunicatedWithServersState++;

            return resultFromServer2;
        }

        private static Text TranslateFromSpanishViaServer1(Text text, Location location)
        {
            if(DateTime.Now.Millisecond < 500)
                throw new Exception("Error");

            return new Text(text.Value.Replace("hola", "hello") + "_1_" + location);
        }
        private static Text TranslateFromSpanishViaServer2(Text text, Location location)
        {
            return new Text(text.Value.Replace("hola", "hello") + "_2_" + location);
        }
    }
}