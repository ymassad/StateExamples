using System;

namespace PassingStateViaRefParameters
{
    class Program
    {
        static void Main(string[] args)
        {
            Server1State server1State = new Server1State(false, DateTime.MinValue);

            FolderProcessingModule.TranslatedDocumentsInFolder("c:\\inputFolder", "c:\\outputFolder", ref server1State);
        }
    }
}
