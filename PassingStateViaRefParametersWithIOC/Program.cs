using System;

namespace PassingStateViaRefParametersWithIOC
{
    class Program
    {
        static void Main(string[] args)
        {
            Server1State server1StateForLocationA = new Server1State(false, DateTime.MinValue);

            FolderProcessingModule.TranslateDocumentsInFolder(
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
                                ref server1StateForLocationA),
                            text => SpanishTextTranslationModule.TranslateFromSpanish(
                                text,
                                Location.A,
                                ref server1StateForLocationA)))));

            Server1State server1StateForLocationB = new Server1State(false, DateTime.MinValue);

            FolderProcessingModule.TranslateDocumentsInFolder(
                "c:\\inputFolder2",
                "c:\\outputFolder2",
                document => DocumentTranslationModule.TranslateDocument(
                    document,
                    paragraph => DocumentTranslationModule.TranslateParagraph(
                        paragraph,
                        paragraphText => DocumentTranslationModule.TranslateText(
                            paragraphText,
                            text => GermanTextTranslationModule.TranslateFromGerman(
                                text,
                                Location.B,
                                ref server1StateForLocationB),
                            text => SpanishTextTranslationModule.TranslateFromSpanish(
                                text,
                                Location.B,
                                ref server1StateForLocationB)))));
        }
    }
}
