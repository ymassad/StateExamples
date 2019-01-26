using System;

namespace UsingGlobalVariables
{
    public static class SpanishTextTranslationModule
    {
        public static Text TranslateFromSpanish(Text text)
        {
            bool useServer1 = true;

            if (GlobalServer1State.Server1IsDown)
            {
                if (DateTime.Now - GlobalServer1State.Server1DownSince < TimeSpan.FromMinutes(10))
                {
                    useServer1 = false;
                }
            }

            if (useServer1)
            {
                try
                {
                    var result = TranslateFromSpanishViaServer1(text);

                    GlobalServer1State.Server1IsDown = false;

                    return result;
                }
                catch
                {
                    GlobalServer1State.Server1IsDown = true;
                    GlobalServer1State.Server1DownSince = DateTime.Now;
                }
            }

            return TranslateFromSpanishViaServer2(text);
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