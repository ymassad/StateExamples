using System;

namespace MultithreadingAndRefStateParametersAndLockStatement
{
    public static class GermanTextTranslationModule
    {
        public static Text TranslateFromGerman(
            Text text,
            Location location,
            ref Server1State server1State,
            ref int numberOfTimesCommunicatedWithServersState,
            object numberOfTimesCommunicatedWithServersStateLockingObject)
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
                    var result = TranslateFromGermanViaServer1(text, location);

                    lock (numberOfTimesCommunicatedWithServersStateLockingObject)
                    {
                        numberOfTimesCommunicatedWithServersState++;
                    }

                    server1State = new Server1State(false, DateTime.MinValue);

                    return result;
                }
                catch
                {
                    server1State = new Server1State(true, DateTime.Now);
                }
            }

            var resultFromServer2 = TranslateFromGermanViaServer2(text, location);

            lock (numberOfTimesCommunicatedWithServersStateLockingObject)
            {
                numberOfTimesCommunicatedWithServersState++;
            }

            return resultFromServer2;
        }

        //...
        private static Text TranslateFromGermanViaServer1(Text text, Location location)
        {
            if (DateTime.Now.Millisecond < 500)
                throw new Exception("Error");
            return new Text(text.Value.Replace("tag", "day") + "_1_" + location);
        }

        private static Text TranslateFromGermanViaServer2(Text text, Location location)
        {
            return new Text(text.Value.Replace("tag", "day") + "_2_" + location);
        }
    }
}