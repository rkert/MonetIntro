using System.Collections.Generic;
using System.Xml.Serialization;

namespace MonetIntro.Data
{
    [XmlRoot(ElementName = "header")]
    public class XmlExportHeader
    {
        [XmlElement(ElementName = "errorCode")]
        public uint ErrorCode { get; set; }

        [XmlElement(ElementName = "version")]
        public uint ExportVersion { get; set; }

        [XmlElement(ElementName = "ItemsCount")]
        public uint ItemsCount { get; set; }

        [XmlElement(ElementName = "BatchNum")]
        public string BatchNumber { get; set; }
    }

    [XmlRoot(ElementName = "Item")]
    public class XmlCard
    {
        [XmlElement(ElementName = "Card_Item_1")]
        public string Surname { get; set; }

        [XmlElement(ElementName = "Card_Item_2")]
        public string Firstname { get; set; }
        
        [XmlElement(ElementName = "Card_Item_4a")]
        public string IssueDate { get; set; }
        
        [XmlElement(ElementName = "Card_Item_4b")]
        public string ExpiryDate { get; set; }
        
        [XmlElement(ElementName = "Card_Item_5b")]
        public string CardNumber { get; set; }
        
        [XmlElement(ElementName = "Card_Holder_Photo")]
        public string CardHolderPhoto { get; set; }

        [XmlElement(ElementName = "Card_Item_8")]
        public string HomeAddress { get; set; }
        
        [XmlElement(ElementName = "Card_TypeID")]
        public uint CardTypeId { get; set; }
        
        [XmlElement(ElementName = "Card_CHR")]
        public string CardSecret { get; set; }
    }

    [XmlRoot(ElementName = "Items")]
    public class XmlCards
    {
        [XmlElement(ElementName = "Item")]
        public List<XmlCard> Items { get; set; }
    }

    [XmlRoot(ElementName = "root")]
    public class XmlCardExport
    {
        [XmlElement(ElementName = "header")]
        public XmlExportHeader Header { get; set; }

        [XmlElement(ElementName = "Items")]
        public XmlCards Items { get; set; }

        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }

        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsd { get; set; }
    }
}
