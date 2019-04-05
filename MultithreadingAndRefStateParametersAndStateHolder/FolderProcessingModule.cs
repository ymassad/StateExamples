using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace MultithreadingAndRefStateParametersAndStateHolder
{
    public static class FolderProcessingModule
    {
        public static void TranslateDocumentsInFolderInParallel(string folderPath, string destinationFolderPath, Func<Document, Document> translateDocument)
        {
            IEnumerable<Document> documentsEnumerable = GetDocumentsFromFolder(folderPath);

            Parallel.ForEach(documentsEnumerable, document =>
            {
                var translatedDocument = translateDocument(document);

                WriteDocumentToDestinationFolder(translatedDocument, destinationFolderPath);
            });
        }
        
        private static void WriteDocumentToDestinationFolder(Document translatedDocument, string destinationFolderPath)
        {
            //Save document
        }

        public static IEnumerable<Document> GetDocumentsFromFolder(string folderPath)
        {
            //Read documents from folder. Here I use dummy data.
            return Enumerable.Range(1, 1000)
                .Select(x => new Document(
                    "My happy document",
                    ImmutableArray<Paragraph>.Empty
                        .Add(new Paragraph(new Text(x + ". " + "hola!"), new Color(255, 0, 0)))
                        .Add(new Paragraph(new Text(x + ". " + "Guten tag!"), new Color(0, 255, 0)))));
        }
    }
}
