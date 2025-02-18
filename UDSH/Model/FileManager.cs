using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
        private const string RootElementLabel = "File";
        private const string MetaElementLabel = "Metadata";
        private const string DocumentElementLabel = "Document";
        private const string ParagraphElementLabel = "ParagraphLine";
        private const string ListElementLabel = "List";
        private const string ListItemElementLabel = "ListItem";

        // Attributes
        private const string FileIDAttribute = "FileID";
        private const string FileNameAttribute = "FileName";
        private const string FileTypeAttribute = "FileType";
        private const string FileAuthorAttribute = "FileAuthor";
        private const string FileVersionAttribute = "FileVersion";
        private const string FileCreationDateAttribute = "FileCreationDate";
        private const string TextAlignment = "TextAlignment";
        private const string TextType = "TextType";

        // Text Size
        private const int NormalFontSize = 20;
        private const int HeaderOneFontSize = 40;
        private const int HeaderTwoFontSize = 30;
        private const int HeaderThreeFontSize = 25;

        public void InitializeNewFileData(XmlWriter xmlWriter, FileSystem file)
        {
            // Keep different file types in mind!!!
            MKMFile mKMFile = new MKMFile();

            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement(RootElementLabel);

            WriteMetadata(xmlWriter, mKMFile, file);

            xmlWriter.WriteStartElement(DocumentElementLabel);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement(); // End Root

            xmlWriter.WriteEndDocument(); // End File
        }

        public void UpdateFileData(XmlWriter xmlWriter, FileSystem file, BlockCollection Blocks)
        {
            MKMFile mKMFile = new MKMFile();

            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement(RootElementLabel);

            WriteMetadata(xmlWriter, mKMFile, file);

            WriteDocument(xmlWriter, file, Blocks, mKMFile);

            xmlWriter.WriteEndElement(); // End Root

            xmlWriter.WriteEndDocument(); // End File
        }

        public bool LoadFileDataContent(FileSystem file, RichTextBox RTB)
        {
            XDocument xDocument = XDocument.Load(file.FileDirectory);
            MKMFile mKMFile = new MKMFile();
            bool ContainData = false;

            if (xDocument.Root != null)
            {
                Paragraph paragraph = new Paragraph();
                Paragraph LastParagraph = new Paragraph();
                BlockCollection Blocks = RTB.Document.Blocks;
                Block CurrentBlock = Blocks.FirstBlock;
                bool FirstLine = true;

                // TODO: Manage Lists
                XElement? DocElement = xDocument.Root.Element(DocumentElementLabel);
                if (DocElement == null)
                    return ContainData;

                foreach (var ParaElement in DocElement.Elements(ParagraphElementLabel))
                {
                    XElement TopElement = ParaElement.Elements().First();
                    if(TopElement.Name.ToString().Equals(ListElementLabel))
                    {
                        WriteListData(ParaElement, Blocks, LastParagraph);
                    }
                    else
                    {
                        WriteContentData(ParaElement, ParaElement, paragraph);

                        Blocks.Add(paragraph);
                        LastParagraph = paragraph;
                        paragraph = new Paragraph();
                    }
                    
                    ContainData = true;
                }
            }

            return ContainData;
        }

        private void WriteListData(XElement ParaElement, BlockCollection Blocks, Paragraph LastParagraph)
        {
            XElement ListElement = ParaElement.Elements().First();
            List list = new List();
            bool FirstItem = true;

            foreach (var listItemElement in ListElement.Elements())
            {
                Paragraph paragraph = new Paragraph();

                WriteContentData(listItemElement, ParaElement, paragraph);

                ListItem listItem = new ListItem(paragraph);
                list.ListItems.Add(listItem);
                
                if(FirstItem == true)
                {
                    Blocks.InsertAfter(LastParagraph, list);
                    FirstItem = false;
                }
            }
        }

        private void WriteContentData(XElement ParentElement, XElement ParaElement, Paragraph paragraph)
        {
            foreach (var WeightElement in ParentElement.Elements())
            {
                Run run = new Run();

                XElement StyleElement = WeightElement.Elements().First();
                XElement DecorationElement = StyleElement.Elements().First();
                string TextContent = DecorationElement.Value;

                run.FontWeight = GetFontWeight(WeightElement.Name.ToString());
                run.FontStyle = GetFontStyle(StyleElement.Name.ToString());
                TextDecorationCollection textDecoration = GetDecorations(DecorationElement.Name.ToString());
                if (textDecoration != TextDecorations.Baseline)
                    run.TextDecorations = textDecoration;

                run.FontSize = GetSize(ParaElement.Attribute(TextType).Value);
                run.Tag = ParaElement.Attribute(TextType).Value;
                run.Text = TextContent;

                paragraph.TextAlignment = GetAlignment(ParaElement.Attribute(TextAlignment).Value);
                paragraph.Tag = ParaElement.Attribute(TextType).Value;
                paragraph.Inlines.Add(run);
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

            xmlWriter.WriteStartElement(MetaElementLabel);
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

            xmlWriter.WriteStartElement(DocumentElementLabel);

            foreach (var block in Blocks)
            {
                if (block is Paragraph paragraph)
                {
                    ParagraphLine paragraphLine = new ParagraphLine();
                    paragraphLine.TextAlignment = paragraph.TextAlignment.ToString();
                    if (paragraph.Tag != null)
                        paragraphLine.TextType = paragraph.Tag.ToString();
                    else
                        paragraphLine.TextType = "Normal";

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

                    xmlWriter.WriteStartElement(ParagraphElementLabel);
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
                    xmlWriter.WriteStartElement(ParagraphElementLabel);
                    ParagraphLine paragraphLine = new ParagraphLine();
                    paragraphLine.TextAlignment = "Left";
                    paragraphLine.TextType = "List";

                    xmlWriter.WriteAttributeString(TextAlignment, paragraphLine.TextAlignment);
                    xmlWriter.WriteAttributeString(TextType, paragraphLine.TextType);

                    xmlWriter.WriteStartElement(ListElementLabel);

                    foreach (var item in list.ListItems)
                    {
                        xmlWriter.WriteStartElement(ListItemElementLabel);
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

        private FontWeight GetFontWeight(string WeightValue)
        {
            switch (WeightValue)
            {
                case "Normal":
                    return FontWeights.Normal;
                case "Bold":
                    return FontWeights.Bold;
                default:
                    return FontWeights.Normal;
            }
        }

        private FontStyle GetFontStyle(string StyleValue)
        {
            switch (StyleValue)
            {
                case "Normal":
                    return FontStyles.Normal;
                case "Italic":
                    return FontStyles.Italic;
                default:
                    return FontStyles.Normal;
            }
        }

        private TextDecorationCollection GetDecorations(string DecorationValue)
        {
            switch (DecorationValue)
            {
                case "Normal":
                    return TextDecorations.Baseline;
                case "Strikethrough":
                    return TextDecorations.Strikethrough;
                case "Underline":
                    return TextDecorations.Underline;
                default:
                    return TextDecorations.Baseline;
            }
        }

        private TextAlignment GetAlignment(string AlignmentValue)
        {
            switch (AlignmentValue)
            {
                case "Left":
                    return System.Windows.TextAlignment.Left;
                case "Center":
                    return System.Windows.TextAlignment.Center;
                case "Right":
                    return System.Windows.TextAlignment.Right;
                default:
                    return System.Windows.TextAlignment.Left;
            }
        }

        private int GetSize(string SizeValue)
        {
            switch (SizeValue)
            {
                case "Normal":
                    return NormalFontSize;
                case "Header1":
                    return HeaderOneFontSize;
                case "Header2":
                    return HeaderTwoFontSize;
                case "Header3":
                    return HeaderThreeFontSize;
                default:
                    return NormalFontSize;
            }
        }
    }
}
