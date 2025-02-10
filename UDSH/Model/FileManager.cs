using System.Diagnostics;
using System.Linq;
using System.Windows.Documents;
using System.Xml;
using System.Xml.Linq;

namespace UDSH.Model
{
    public class MKMFile
    {
        public Metadata Metadata { get; set; } = new Metadata();
        public Document Document { get; set; } = new Document();
    }

    public class FileManager
    {
        // Main Elements
        private const string RootElement = "File";
        private const string MetaElement = "Metadata";
        private const string DocumentElement = "Document";
        private const string ParagraphElement = "ParagraphLine";
        private const string ListElement = "List";
        private const string ListItemElement = "ListItem";

        // Attributes
        private const string FileIDAttribute = "FileID";
        private const string FileNameAttribute = "FileName";
        private const string FileTypeAttribute = "FileType";
        private const string FileAuthorAttribute = "FileAuthor";
        private const string FileVersionAttribute = "FileVersion";
        private const string FileCreationDateAttribute = "FileCreationDate";
        private const string TextAlignment = "TextAlignment";
        private const string TextType = "TextType";

        public void InitializeNewFileData(XmlWriter xmlWriter, FileSystem file)
        {
            // Keep different file types in mind!!!
            MKMFile mKMFile = new MKMFile();

            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement(RootElement);

            WriteMetadata(xmlWriter, mKMFile, file);

            xmlWriter.WriteStartElement(DocumentElement);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement(); // End Root

            xmlWriter.WriteEndDocument(); // End File
        }

        public void UpdateFileData(XmlWriter xmlWriter, FileSystem file, BlockCollection Blocks)
        {
            MKMFile mKMFile = new MKMFile();

            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement(RootElement);

            WriteMetadata(xmlWriter, mKMFile, file);

            WriteDocument(xmlWriter, file, Blocks, mKMFile);

            xmlWriter.WriteEndElement(); // End Root

            xmlWriter.WriteEndDocument(); // End File
        }

        public void LoadFileDataContent(FileSystem file)
        {
            XDocument xDocument = XDocument.Load(file.FileDirectory);
            MKMFile mKMFile = new MKMFile();

            if (xDocument.Root != null)
            {
                XElement? DocElement = xDocument.Root.Element(DocumentElement);
                foreach (var ParaElement in DocElement.Elements(ParagraphElement))
                {
                    ParagraphLine paragraphLine = new ParagraphLine
                    {
                        TextAlignment = ParaElement.Attribute(TextAlignment).Value,
                        TextType = ParaElement.Attribute(TextType).Value,
                        Content = ParaElement.Elements().ToList()
                    };
                    mKMFile.Document.ParagraphLines.Add(paragraphLine);

                    // TODO: write to the paragraph in this loop instead of loading in document. (loop through Content)
                }
            }
        }

        private void WriteMetadata(XmlWriter xmlWriter, MKMFile mKMFile, FileSystem file)
        {
            mKMFile.Metadata.FileID = file.FileID;
            mKMFile.Metadata.FileName = file.FileName;
            mKMFile.Metadata.FileType = file.FileType;
            mKMFile.Metadata.FileAuthor = file.FileAuthor;
            mKMFile.Metadata.FileVersion = file.FileVersion;
            mKMFile.Metadata.FileCreationDate = file.FileCreationDate;

            xmlWriter.WriteStartElement(MetaElement);
            xmlWriter.WriteElementString(FileIDAttribute, mKMFile.Metadata.FileID);
            xmlWriter.WriteElementString(FileNameAttribute, mKMFile.Metadata.FileName);
            xmlWriter.WriteElementString(FileTypeAttribute, mKMFile.Metadata.FileType);
            xmlWriter.WriteElementString(FileAuthorAttribute, mKMFile.Metadata.FileAuthor);
            xmlWriter.WriteElementString(FileVersionAttribute, mKMFile.Metadata.FileVersion);
            xmlWriter.WriteElementString(FileCreationDateAttribute, mKMFile.Metadata.FileCreationDate.ToString());
            xmlWriter.WriteEndElement();
        }

        private void WriteDocument(XmlWriter xmlWriter, FileSystem file, BlockCollection Blocks, MKMFile mKMFile)
        {
            Document document = new Document();

            xmlWriter.WriteStartElement(DocumentElement);

            foreach (var block in Blocks)
            {
                if (block is Paragraph paragraph)
                {
                    ParagraphLine paragraphLine = new ParagraphLine();
                    paragraphLine.TextAlignment = paragraph.TextAlignment.ToString();
                    paragraphLine.TextType = paragraph.Tag.ToString();

                    foreach (var inline in paragraph.Inlines)
                    {
                        if (inline is Run run)
                        {
                            paragraphLine.Content.Add(new XElement(run.FontWeight.ToString(), // Weight
                                new XElement(run.FontStyle.ToString(),                        // Style
                                new XElement(GetTextDecorations(run), run.Text))));           // Decorations

                            Debug.WriteLine($"Text = {run.Text} and Its weight = {run.FontWeight}");
                        }
                    }

                    xmlWriter.WriteStartElement(ParagraphElement);
                    xmlWriter.WriteAttributeString(TextAlignment, paragraphLine.TextAlignment);
                    xmlWriter.WriteAttributeString(TextType, paragraphLine.TextType);

                    foreach(var element in paragraphLine.Content)
                    {
                        element.WriteTo(xmlWriter);
                    }
                    xmlWriter.WriteEndElement(); // End Paragraph
                }
                else if (block is List list)
                {
                    xmlWriter.WriteStartElement(ParagraphElement);
                    ParagraphLine paragraphLine = new ParagraphLine();
                    paragraphLine.TextAlignment = "Left";
                    paragraphLine.TextType = "List";

                    xmlWriter.WriteAttributeString(TextAlignment, paragraphLine.TextAlignment);
                    xmlWriter.WriteAttributeString(TextType, paragraphLine.TextType);

                    xmlWriter.WriteStartElement(ListElement);

                    foreach (var item in list.ListItems)
                    {
                        xmlWriter.WriteStartElement(ListItemElement);
                        if (item.Blocks.FirstBlock is Paragraph listParagraph)
                        {
                            foreach (var inline in listParagraph.Inlines)
                            {
                                if (inline is Run run)
                                {
                                    paragraphLine.Content.Add(new XElement(run.FontWeight.ToString(), // Weight
                                        new XElement(run.FontStyle.ToString(),                        // Style
                                        new XElement(GetTextDecorations(run), run.Text))));           // Decorations

                                    Debug.WriteLine($"Text = {run.Text} and Its weight = {run.FontWeight}");
                                }
                            }
                            
                            foreach (var element in paragraphLine.Content)
                            {
                                element.WriteTo(xmlWriter);
                            }
                            paragraphLine.Content.Clear();
                        }
                        xmlWriter.WriteEndElement(); // End ListItem
                    }

                    xmlWriter.WriteEndElement(); // End List
                    xmlWriter.WriteEndElement(); // End Paragraph
                }
            }

            xmlWriter.WriteEndElement(); // End Document
        }

        private string GetTextDecorations(Run run)
        {
            if (run.TextDecorations != null || run.TextDecorations.Count > 0)
            {
                string decoration = string.Join(", ", run.TextDecorations.Select(d => d.Location.ToString()));
                if (!string.IsNullOrEmpty(decoration))
                    return decoration;
                else
                    return "Normal";
            }
            else
                return "Normal";
        }
    }
}
