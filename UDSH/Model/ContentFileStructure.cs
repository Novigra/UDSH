// Copyright (C) 2025 Mohammed Kenawy
using System.Windows.Media.Imaging;

namespace UDSH.Model
{
    /// <summary>
    /// Class responsible for data display in Content Window
    /// </summary>
    public class ContentFileStructure
    {
        public string Name { get; set; }
        public DateTime LastDateModification { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
        public string Author { get; set; }
        public string Directory { get; set; } // Directory for (FOLDERS). Files directory is available in (FileSystem - File)
        public BitmapImage Image { get; set; }
        public BitmapImage LargeNormalImage { get; set; }
        public BitmapImage LargeHighlightImage { get; set; }
        public FileSystem File { get; set; } // For quick access and modification
    }
}
