using CodingAssignmentLib.Abstractions;
using System.Xml.Linq;

namespace CodingAssignmentLib
{
    public class XmlContentParser : IContentParser
    {
        public IEnumerable<Data> Parse(string content)
        {
            var document = XDocument.Parse(content);
            return document
                .Descendants("Data")
                .Select(data => new Data(
                    data.Element("Key")?.Value ?? string.Empty,
                    data.Element("Value")?.Value ?? string.Empty
                ));
        }
    }
}
