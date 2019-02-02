using System;

namespace PassingStateViaRefParametersWithIOC
{
    class Program
    {
        static void Main(string[] args)
        {
Server1State server1StateForLocationA = new Server1State(false, DateTime.MinValue);

            Func<Text, Text> translateFromGerman = text => GermanTextTranslationModule.TranslateFromGerman(
                text,
                Location.A,
                ref server1StateForLocationA);


            Func<Text, Text> translateFromSpanish = text => SpanishTextTranslationModule.TranslateFromSpanish(
                text,
                Location.A,
                ref server1StateForLocationA);

            Func<Text, Text> translateText = paragraphText => DocumentTranslationModule.TranslateText(
                paragraphText,
                translateFromGerman: translateFromGerman,
                translateFromSpanish: translateFromSpanish);


            Func<Paragraph, Paragraph> translateParagraph = paragraph => DocumentTranslationModule.TranslateParagraph(
                paragraph,
                translateText);

            Func<Document, Document> translateDocument = document => DocumentTranslationModule.TranslateDocument(
                document,
                translateParagraph);


            FolderProcessingModule.TranslateDocumentsInFolder(
                "c:\\inputFolder1",
                "c:\\outputFolder1",
                translateDocument);

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
