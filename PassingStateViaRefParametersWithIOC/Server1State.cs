using System;

namespace PassingStateViaRefParametersWithIOC
{

    public class Server1State
    {
        public Server1State(bool server1IsDown, DateTime server1DownSince)
        {
            Server1IsDown = server1IsDown;
            Server1DownSince = server1DownSince;
        }

        public DateTime Server1DownSince { get; }
        public bool Server1IsDown { get; }
    }
}
