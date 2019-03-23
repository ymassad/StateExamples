using System;

namespace MultithreadingAndRefStateParameters
{
    class Program
    {
        static void Main(string[] args)
        {
            Server1State server1StateForLocationA = new Server1State(false, DateTime.MinValue);

            int numberOfTimesCommunicatedWithServersState = 0;

            FolderProcessingModule.TranslateDocumentsInFolderInParallel(
                "c:\\inputFolder1",
                "c:\\outputFolder1",
                document => DocumentTranslationModule.TranslateDocument(
                    document,
                    paragraph => DocumentTranslationModule.TranslateParagraph(
                        paragraph,
                        paragraphText => DocumentTranslationModule.TranslateText(
                            paragraphText,
                            text => GermanTextTranslationModule.TranslateFromGerman(
                                text,
                                Location.A,
                                ref server1StateForLocationA,
                                ref numberOfTimesCommunicatedWithServersState),
                            text => SpanishTextTranslationModule.TranslateFromSpanish(
                                text,
                                Location.A,
                                ref server1StateForLocationA,
                                ref numberOfTimesCommunicatedWithServersState)))));

            Console.WriteLine(
                "Number of times communicated with servers: "
                + numberOfTimesCommunicatedWithServersState);
        }
    }
}
