using System;

namespace UsingGlobalVariables
{

    public static class GlobalServer1State
    {
        public static DateTime Server1DownSince { get; set; }
        public static bool Server1IsDown { get; set; }
    }
}
