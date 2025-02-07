using System.Xml;

namespace UDSH.Model
{
    public class MKMFile
    {
        public Metadata Metadata { get; set; } = new Metadata();
        public List<Document> Documents { get; set; } = new List<Document>();
    }

    public class FileManager
    {
        // Main Elements
        private const string RootElement = "File";
        private const string MetaElement = "Metadata";
        private const string DocumentElement = "Document";

        // Attributes
        private const string FileIDAttribute = "FileID";
        private const string FileNameAttribute = "FileName";
        private const string FileTypeAttribute = "FileType";
        private const string FileAuthorAttribute = "FileAuthor";
        private const string FileVersionAttribute = "FileVersion";
        private const string FileCreationDateAttribute = "FileCreationDate";

        public void InitializeNewFileData(XmlWriter xmlWriter, FileSystem file)
        {
            // Keep different file types in mind!!!
            MKMFile mKMFile = new MKMFile();
            mKMFile.Metadata.FileID = file.FileID;
            mKMFile.Metadata.FileName = file.FileName;
            mKMFile.Metadata.FileType = file.FileType;
            mKMFile.Metadata.FileAuthor = file.FileAuthor;
            mKMFile.Metadata.FileVersion = file.FileVersion;
            mKMFile.Metadata.FileCreationDate = file.FileCreationDate;

            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement(RootElement);

            xmlWriter.WriteStartElement(MetaElement);
            xmlWriter.WriteElementString(FileIDAttribute, mKMFile.Metadata.FileID);
            xmlWriter.WriteElementString(FileNameAttribute, mKMFile.Metadata.FileName);
            xmlWriter.WriteElementString(FileTypeAttribute, mKMFile.Metadata.FileType);
            xmlWriter.WriteElementString(FileAuthorAttribute, mKMFile.Metadata.FileAuthor);
            xmlWriter.WriteElementString(FileVersionAttribute, mKMFile.Metadata.FileVersion);
            xmlWriter.WriteElementString(FileCreationDateAttribute, mKMFile.Metadata.FileCreationDate.ToString());
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement(DocumentElement);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement(); // End Root

            xmlWriter.WriteEndDocument(); // End File
        }
    }
}
