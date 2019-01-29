using System;

namespace PassingStateViaStateHolderParameters
{
    class Program
    {
        static void Main(string[] args)
        {
            var server1StateHolder = new StateHolder<Server1State>(
                new Server1State(false, DateTime.MinValue));

            FolderProcessingModule.TranslatedDocumentsInFolder(
                "c:\\inputFolder",
                "c:\\outputFolder", server1StateHolder);
        }
    }
}
