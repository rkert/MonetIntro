using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using MonetIntro.Exceptions;

namespace MonetIntro.Data
{
    public class XmlManagerService
    {
        private const string xmlPath = "xmls\\";
        public string[] GetAvailableXmls()
        {
            return Directory.GetFiles(xmlPath, "*.xml").Select(Path.GetFileName).ToArray();
        }

        public XmlCardExport GetXmlByName(string xmlName = "test.xml")
        {
            var serializer = new XmlSerializer(typeof(XmlCardExport));
            return (XmlCardExport)serializer.Deserialize(new XmlTextReader($"xmls\\{xmlName}"));
        }

        public async Task AddXml(string fileName, Stream data)
        {
            var sr = new StreamReader(data);
            var text = await sr.ReadToEndAsync();

            if (!fileName.ToLower().EndsWith(".xml"))
                throw new FileUploadException("Vstupní soubor není XML.");

            if (File.Exists($"{xmlPath}{fileName}"))
                throw new FileUploadException("Nahrávaný soubor již existuje.");

            try
            {
                CheckStringXml(text);
            }
            catch (Exception)
            {
                throw new FileUploadException("Vstupní XML není validní.");
            }

            await using var sw = new StreamWriter($"{xmlPath}{fileName}");
            await sw.WriteAsync(text);
            await sw.FlushAsync();
            sw.Close();
        }

        private void CheckStringXml(string xml)
        {
            var serializer = new XmlSerializer(typeof(XmlCardExport));
            var _ = (XmlCardExport)serializer.Deserialize(new StringReader(xml));
        }
    }
}
