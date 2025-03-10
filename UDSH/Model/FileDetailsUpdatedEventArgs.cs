// Copyright (C) 2025 Mohammed Kenawy
namespace UDSH.Model
{
    public class FileDetailsUpdatedEventArgs : EventArgs
    {
        public ContentFileStructure FileStructure { get; }
        public string OldDirectory { get; }
        public int CurrentLevel { get; }

        public FileDetailsUpdatedEventArgs(ContentFileStructure fileStructure, string oldDirectory, int currentLevel)
        {
            FileStructure = fileStructure;
            OldDirectory = oldDirectory;
            CurrentLevel = currentLevel;
        }
    }
}
