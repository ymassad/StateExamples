using System.Collections.Generic;
using System.Collections.Immutable;

namespace PassingStateViaRefParameters
{
    public static class DocumentTranslationModule
    {
        public static Document TranslateDocument(Document document, ref Server1State server1State)
        {
            var paragraphs = document.Paragraphs;

            List<Paragraph> translatedParagraphs =  new List<Paragraph>();

            foreach (var paragraph in paragraphs)
            {
                translatedParagraphs.Add(TranslateParagraph(paragraph, ref server1State));
            }
            
            return new Document(document.Title, translatedParagraphs.ToImmutableArray());
        }

        public static Paragraph TranslateParagraph(Paragraph paragraph, ref Server1State server1State)
        {
            return new Paragraph(TranslateText(paragraph.Text, ref server1State), paragraph.Color);
        }

        public static Text TranslateText(Text paragraphText, ref Server1State server1State)
        {
            var language = DetectLanguage(paragraphText);

            return language == Language.German
                ? GermanTextTranslationModule.TranslateFromGerman(paragraphText, ref server1State)
                : SpanishTextTranslationModule.TranslateFromSpanish(paragraphText, ref server1State);
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
