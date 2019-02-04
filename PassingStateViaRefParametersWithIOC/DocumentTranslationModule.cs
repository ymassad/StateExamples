using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace PassingStateViaRefParametersWithIOC
{
    public static class DocumentTranslationModule
    {
        public static Document TranslateDocument(Document document, Func<Paragraph, Paragraph> translateParagraph)
        {
            var paragraphs = document.Paragraphs;

            List<Paragraph> translatedParagraphs =  new List<Paragraph>();

            foreach (var paragraph in paragraphs)
            {
                translatedParagraphs.Add(translateParagraph(paragraph));
            }
            
            return new Document(document.Title, translatedParagraphs.ToImmutableArray());
        }

        public static Paragraph TranslateParagraph(Paragraph paragraph, Func<Text, Text> translateText)
        {
            return new Paragraph(translateText(paragraph.Text), paragraph.Color);
        }

        public static Text TranslateText(Text paragraphText, Func<Text, Text> translateFromGerman, Func<Text, Text> translateFromSpanish)
        {
            var language = DetectLanguage(paragraphText);

            return language == Language.German
                ? translateFromGerman(paragraphText)
                : translateFromSpanish(paragraphText);
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
