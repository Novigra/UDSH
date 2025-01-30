namespace UDSH.Model
{
    public class DataDragActionUpdateEventArgs : EventArgs
    {
        public List<ContentFileStructure> SelectedItems { get; set; }
        public ContentFileStructure TargetItem { get; set; }
        public string CurrentDirectory { get; set; }

        public DataDragActionUpdateEventArgs(List<ContentFileStructure> selectedItems, ContentFileStructure targetItem, string currentDirectory)
        {
            SelectedItems = selectedItems;
            TargetItem = targetItem;
            CurrentDirectory = currentDirectory;
        }
    }
}
