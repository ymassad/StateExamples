using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace PassingStateViaParametersAndReturnValues
{
    public static class DocumentTranslationModule
    {
        public static (Document result, Server1State newServer1State) TranslateDocument(Document document, Server1State server1State)
        {
            var paragraphs = document.Paragraphs;
            
            List<Paragraph> translatedParagraphs =  new List<Paragraph>();

            var currentState = server1State;

            foreach (var paragraph in paragraphs)
            {
                (var translateParagraph, var newServer1State) = TranslateParagraph(paragraph, currentState);

                currentState = newServer1State;

                translatedParagraphs.Add(translateParagraph);
            }
            
            return (new Document(document.Title, translatedParagraphs.ToImmutableArray()), currentState);
        }

        public static (Paragraph result, Server1State newServer1State)  TranslateParagraph(Paragraph paragraph, Server1State server1State)
        {
            var (result, newServer1State) = TranslateText(paragraph.Text, server1State);

            return (new Paragraph(result, paragraph.Color), newServer1State);
        }

        public static (Text result, Server1State newServer1State) TranslateText(Text paragraphText, Server1State server1State)
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
