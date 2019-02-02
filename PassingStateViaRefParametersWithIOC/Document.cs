using System.Collections.Immutable;

namespace PassingStateViaRefParametersWithIOC
{
    public class Document
    {
        public string Title { get; }

        public ImmutableArray<Paragraph> Paragraphs { get; }

        public Document(string title, ImmutableArray<Paragraph> paragraphs)
        {
            Paragraphs = paragraphs;
            Title = title;
        }
    }
}