using System;

namespace PassingStateViaParametersAndReturnValues
{
    public static class SpanishTextTranslationModule
    {
        public static (Text result, Server1State newServer1State) TranslateFromSpanish(Text text, Server1State server1State)
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
                    var result = TranslateFromSpanishViaServer1(text);

                    return (result, new Server1State(false, DateTime.MinValue));
                }
                catch
                {
                    return (TranslateFromSpanishViaServer2(text), new Server1State(true, DateTime.Now));
                }
            }

            return (TranslateFromSpanishViaServer2(text), server1State);
        }

        private static Text TranslateFromSpanishViaServer1(Text text)
        {
            if(DateTime.Now.Millisecond < 500)
                throw new Exception("Error");

            return new Text(text.Value.Replace("hola", "hello") + "_1");
        }
        private static Text TranslateFromSpanishViaServer2(Text text)
        {
            return new Text(text.Value.Replace("hola", "hello") + "_2");
        }
    }
}