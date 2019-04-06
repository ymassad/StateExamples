using System;

namespace MultithreadingAndRefStateParameters
{
    public static class GermanTextTranslationModule
    {
        public static Text TranslateFromGerman(
            Text text,
            Location location,
            ref int numberOfTimesCommunicatedWithServersState)
        {
            bool useServer1 = DateTime.Now.Millisecond < 500;

            if (useServer1)
            {
                var result = TranslateFromGermanViaServer1(text, location);

                numberOfTimesCommunicatedWithServersState++;

                return result;
            }

            var resultFromServer2 = TranslateFromGermanViaServer2(text, location);

            numberOfTimesCommunicatedWithServersState++;

            return resultFromServer2;
        }

        private static Text TranslateFromGermanViaServer1(Text text, Location location)
        {
            return new Text(text.Value.Replace("tag", "day") + "_1_" + location);
        }

        private static Text TranslateFromGermanViaServer2(Text text, Location location)
        {
            return new Text(text.Value.Replace("tag", "day") + "_2_" + location);
        }
    }
}