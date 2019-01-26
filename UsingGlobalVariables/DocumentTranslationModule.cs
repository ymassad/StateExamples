using System.Collections.Immutable;
using System.Linq;

namespace UsingGlobalVariables
{
    public static class DocumentTranslationModule
    {
        public static Document TranslateDocument(Document document)
        {
            var paragraphs = document.Paragraphs;

            return new Document(document.Title, paragraphs.Select(TranslateParagraph).ToImmutableArray());
        }

        public static Paragraph TranslateParagraph(Paragraph paragraph)
        {
            return new Paragraph(TranslateText(paragraph.Text), paragraph.Color);
        }

        public static Text TranslateText(Text paragraphText)
        {
            var language = DetectLanguage(paragraphText);

            return language == Language.German
                ? GermanTextTranslationModule.TranslateFromGerman(paragraphText)
                : SpanishTextTranslationModule.TranslateFromSpanish(paragraphText);
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
