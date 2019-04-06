using System;

namespace MultithreadingAndRefStateParameters
{
    public static class SpanishTextTranslationModule
    {
        public static Text TranslateFromSpanish(
            Text text,
            Location location,
            ref int numberOfTimesCommunicatedWithServersState)
        {
            bool useServer1 = DateTime.Now.Millisecond < 500;

            if (useServer1)
            {
                var result = TranslateFromSpanishViaServer1(text, location);

                numberOfTimesCommunicatedWithServersState++;

                return result;
            }

            var resultFromServer2 = TranslateFromSpanishViaServer2(text, location);

            numberOfTimesCommunicatedWithServersState++;

            return resultFromServer2;
        }

        private static Text TranslateFromSpanishViaServer1(Text text, Location location)
        {
            return new Text(text.Value.Replace("hola", "hello") + "_1_" + location);
        }

        private static Text TranslateFromSpanishViaServer2(Text text, Location location)
        {
            return new Text(text.Value.Replace("hola", "hello") + "_2_" + location);
        }
    }
}