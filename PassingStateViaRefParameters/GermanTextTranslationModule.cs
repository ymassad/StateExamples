﻿using System;

namespace PassingStateViaRefParameters
{
    public static class GermanTextTranslationModule
    {
        public static Text TranslateFromGerman(Text text, ref Server1State server1State)
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
                    var result = TranslateFromGermanViaServer1(text);

                    server1State = new Server1State(false, DateTime.MinValue);

                    return result;
                }
                catch
                {
                    server1State = new Server1State(true, DateTime.Now);
                }
            }

            return TranslateFromGermanViaServer2(text);
        }

        //...
        private static Text TranslateFromGermanViaServer1(Text text)
        {
            if (DateTime.Now.Millisecond < 500)
                throw new Exception("Error");
            return new Text(text.Value.Replace("tag", "day") + "_1");
        }

        private static Text TranslateFromGermanViaServer2(Text text)
        {
            return new Text(text.Value.Replace("tag", "day") + "_2");
        }
    }
}