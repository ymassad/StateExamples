using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace MultithreadingAndUsingReturnValue
{
    public static class FolderProcessingModule
    {
        public static ServerCommunicationStatistics TranslateDocumentsInFolderInParallel(
            string folderPath,
            string destinationFolderPath,
            Location location,
            ServerCommunicationStatistics statisticsState)
        {
            IEnumerable<Document> documentsEnumerable = GetDocumentsFromFolder(folderPath);

            object lockingObject = new object();
            
            Parallel.ForEach(
                documentsEnumerable,
                () => ServerCommunicationStatistics.Zero(),
                (document, loopState, localState) =>
                {
                    var result = DocumentTranslationModule.TranslateDocument(
                        document, location, localState);

                    WriteDocumentToDestinationFolder(result.document, destinationFolderPath);
      
                    return result.newState;
                },
                (localSum) =>
                {
                    lock (lockingObject)
                    {
                        statisticsState = statisticsState.Combine(localSum);
                    }
                }
            );

            return statisticsState;
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
