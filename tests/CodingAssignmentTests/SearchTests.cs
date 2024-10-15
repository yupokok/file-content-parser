using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;

public class Search
{
    private readonly FileUtility _fileUtility;
    private readonly CsvContentParser _csvParser;
    private readonly JsonContentParser _jsonParser;
    private readonly XmlContentParser _xmlParser;

    public Search(FileUtility fileUtility, CsvContentParser csvParser, JsonContentParser jsonParser, XmlContentParser xmlParser)
    {
        _fileUtility = fileUtility;
        _csvParser = csvParser;
        _jsonParser = jsonParser;
        _xmlParser = xmlParser;
    }

    public List<string> Execute(string directoryPath, string searchTerm)
    {
        var allowedExtensions = new[] { ".csv", ".json", ".xml" };
        var results = new List<string>();

        var files = _fileUtility.GetAllFiles(directoryPath, allowedExtensions);
        foreach (var file in files)
        {
            var extension = _fileUtility.GetExtension(file);
            var content = _fileUtility.GetContent(file);
            IEnumerable<Data> dataList = Enumerable.Empty<Data>();

            if (extension == ".csv")
            {
                dataList = _csvParser.Parse(content);
            }
            else if (extension == ".json")
            {
                dataList = _jsonParser.Parse(content);
            }
            else if (extension == ".xml")
            {
                dataList = _xmlParser.Parse(content);
            }

            var matches = dataList
                .Where(data => data.Key.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase))
                .Select(data => $"Key:{data.Key} Value:{data.Value} FileName:{file}");

            results.AddRange(matches);
        }

        return results;
    }
}
