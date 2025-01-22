namespace UDSH.Model
{
    public class FileDetailsUpdatedEventArgs : EventArgs
    {
        public ContentFileStructure FileStructure { get; }
        public string OldDirectory { get; }

        public FileDetailsUpdatedEventArgs(ContentFileStructure fileStructure, string oldDirectory)
        {
            FileStructure = fileStructure;
            OldDirectory = oldDirectory;
        }
    }
}
