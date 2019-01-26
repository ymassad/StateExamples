using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace PassingStateViaParametersAndReturnValues
{
    public static class FolderProcessingModule
    {
        public static Server1State TranslatedDocumentsInFolder(string folderPath, string destinationFolderPath, Server1State server1State)
        {
            IEnumerable<Document> documentsEnumerable = GetDocumentsFromFolder(folderPath);

            var currentState = server1State;

            foreach (var document in documentsEnumerable)
            {
                var (translatedDocument, newServer1State) = DocumentTranslationModule.TranslateDocument(document, currentState);

                currentState = newServer1State;

                WriteDocumentToDestinationFolder(translatedDocument, destinationFolderPath);
            }

            return currentState;
        }

        private static void WriteDocumentToDestinationFolder(Document translatedDocument, string destinationFolderPath)
        {
            //Save document
        }

        public static IEnumerable<Document> GetDocumentsFromFolder(string folderPath)
        {
            //Read documents from folder. Here I use dummy data.
            return Enumerable.Range(1, 10)
                .Select(x => new Document(
                    "My happy document",
                    ImmutableArray<Paragraph>.Empty
                        .Add(new Paragraph(new Text(x + ". " + "hola!"), new Color(255, 0, 0)))
                        .Add(new Paragraph(new Text(x + ". " + "Guten tag!"), new Color(0, 255, 0)))));
        }
    }
}
