using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace MultithreadingAndUsingReturnValue
{
    public static class DocumentTranslationModule
    {
        public static (Document document, ServerCommunicationStatistics newState) TranslateDocument(
            Document document,
            Location location,
            ServerCommunicationStatistics statisticsState)
        {
            var paragraphs = document.Paragraphs;

            List<Paragraph> translatedParagraphs =  new List<Paragraph>();

            foreach (var paragraph in paragraphs)
            {
                var result = TranslateParagraph(paragraph, location, statisticsState);

                statisticsState = result.newState;

                translatedParagraphs.Add(result.paragraph);
            }
            
            return (new Document(document.Title, translatedParagraphs.ToImmutableArray()), statisticsState);
        }

        public static (Paragraph paragraph, ServerCommunicationStatistics newState) TranslateParagraph(
            Paragraph paragraph,
            Location location,
            ServerCommunicationStatistics statisticsState)
        {
            var result = TranslateText(paragraph.Text, location, statisticsState);

            return (new Paragraph(result.text, paragraph.Color), result.newState);
        }

        public static (Text text, ServerCommunicationStatistics newState) TranslateText(
            Text paragraphText,
            Location location,
            ServerCommunicationStatistics statisticsState)
        {
            var language = DetectLanguage(paragraphText);

            return language == Language.German
                ? GermanTextTranslationModule.TranslateFromGerman(paragraphText, location, statisticsState)
                : SpanishTextTranslationModule.TranslateFromSpanish(paragraphText, location, statisticsState);
        }

        public static Language DetectLanguage(Text paragraphText)
        {
            return
                paragraphText.Value.Contains("hola")
                    ? Language.Spanish
                    : Language.German;
        }
    }
}
