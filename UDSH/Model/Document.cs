using System.Xml.Linq;
using System.Xml.Serialization;

namespace UDSH.Model
{
    public class ParagraphLine
    {
        [XmlAttribute]
        public string TextAlignment { get; set; }

        [XmlAttribute]
        public string TextType { get; set; }

        [XmlAnyElement]
        public List<XElement> Content { get; set; } = new List<XElement>();
    }

    public class Document
    {
        public List<ParagraphLine> ParagraphLines { get; set; } = new List<ParagraphLine>();
    }
}
