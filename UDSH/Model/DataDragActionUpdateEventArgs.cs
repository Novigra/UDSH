namespace UDSH.Model
{
    public class DataDragActionUpdateEventArgs : EventArgs
    {
        public List<ContentFileStructure> SelectedItems { get; set; }
        public ContentFileStructure TargetItem { get; set; }
        public string CurrentDirectory { get; set; }
        public Queue<FileSystem> EditFiles { get; set; }

        public DataDragActionUpdateEventArgs(List<ContentFileStructure> selectedItems, ContentFileStructure targetItem, string currentDirectory, Queue<FileSystem> editFiles)
        {
            SelectedItems = selectedItems;
            TargetItem = targetItem;
            CurrentDirectory = currentDirectory;
            EditFiles = editFiles;
        }
    }
}
