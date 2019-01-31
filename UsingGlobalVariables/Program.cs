namespace UsingGlobalVariables
{
    class Program
    {
        static void Main(string[] args)
        {
            FolderProcessingModule.TranslateDocumentsInFolder("c:\\inputFolder", "c:\\outputFolder");
        }
    }
}
