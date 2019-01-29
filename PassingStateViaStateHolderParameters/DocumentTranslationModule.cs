using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace PassingStateViaStateHolderParameters
{
    public static class DocumentTranslationModule
    {
        public static Document TranslateDocument(Document document, IStateHolder<Server1State> server1State)
        {
            var paragraphs = document.Paragraphs;

            var translatedParagraphs = paragraphs
                .Select(paragraph => TranslateParagraph(paragraph, server1State))
                .ToImmutableArray();

            return new Document(document.Title, translatedParagraphs);
        }

        public static Paragraph TranslateParagraph(Paragraph paragraph, IStateHolder<Server1State> server1State)
        {
            return new Paragraph(TranslateText(paragraph.Text, server1State), paragraph.Color);
        }

        public static Text TranslateText(Text paragraphText, IStateHolder<Server1State> server1State)
        {
            var language = DetectLanguage(paragraphText);

            return language == Language.German
                ? GermanTextTranslationModule.TranslateFromGerman(paragraphText, server1State)
                : SpanishTextTranslationModule.TranslateFromSpanish(paragraphText, server1State);
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
