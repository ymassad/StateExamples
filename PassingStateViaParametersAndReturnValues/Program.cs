using System;

namespace PassingStateViaParametersAndReturnValues
{
    class Program
    {
        static void Main(string[] args)
        {
            Server1State server1State = new Server1State(false, DateTime.MinValue);

            var newServer1State = FolderProcessingModule.TranslateDocumentsInFolder(
                "c:\\inputFolder", "c:\\outputFolder", server1State);
        }
    }
}
