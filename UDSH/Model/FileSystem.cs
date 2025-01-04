using System.Text.Json.Serialization;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace UDSH.Model
{
    public class FileSystem
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FileAuthor { get; set; }
        public string FileVersion { get; set; }
        public string FileDirectory { get; set; }
        public bool IsLastOpenedFile { get; set; }
        public DateTime FileCreationDate { get; set; }
        public DateTime FileLastModificationDate { get; set; }

        [JsonIgnore]
        public BitmapImage fileImageNormal { get; set; } // Image Icon - Normal(Not Selected Item)
        
        [JsonIgnore]
        public BitmapImage fileImageSelected { get; set; } // Image Icon - Normal(Not Selected Item)

        [JsonIgnore]
        public UserControl userControl { get; set; }
    }
}
