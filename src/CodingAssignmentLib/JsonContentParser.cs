using CodingAssignmentLib.Abstractions;
using System.Text.Json;

namespace CodingAssignmentLib
{
    public class JsonContentParser : IContentParser
    {
        public IEnumerable<Data> Parse(string content)
        {
            var items = JsonSerializer.Deserialize<List<JsonItem>>(content);
            return items.Select(item => new Data(item.Key, item.Value));
        }
        private class JsonItem
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }
    }
}
