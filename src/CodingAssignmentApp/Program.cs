// See https://aka.ms/new-console-template for more information

using System.IO.Abstractions;
using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;

Console.WriteLine("Coding Assignment!");

do
{
    Console.WriteLine("\n---------------------------------------\n");
    Console.WriteLine("Choose an option from the following list:");
    Console.WriteLine("\t1 - Display");
    Console.WriteLine("\t2 - Search");
    Console.WriteLine("\t3 - Exit");

    switch (Console.ReadLine())
    {
        case "1":
            Display();
            break;
        case "2":
            Search();
            break;
        case "3":
            return;
        default:
            return;
    }
} while (true);


void Display()
{
    Console.WriteLine("Enter the name of the file to display its content:");

    var fileName = Console.ReadLine()!;
    var fileUtility = new FileUtility(new FileSystem());
    var dataList = Enumerable.Empty<Data>();

    if (fileUtility.GetExtension(fileName) == ".csv")
    {
        dataList = new CsvContentParser().Parse(fileUtility.GetContent(fileName));
    }

    else if (fileUtility.GetExtension(fileName) == ".json")
    {
        dataList = new JsonContentParser().Parse(fileUtility.GetContent(fileName));
    }
    else if (fileUtility.GetExtension(fileName) == ".xml")
    {
        dataList = new XmlContentParser().Parse(fileUtility.GetContent(fileName));
    }

    Console.WriteLine("Data:");

    foreach (var data in dataList)
    {
        Console.WriteLine($"Key:{data.Key} Value:{data.Value}");
    }
}


void Search()
{
    Console.WriteLine("Enter the key to search.");
    var searchTerm = Console.ReadLine()?.Trim();
    var dataDirectory = "data";
    var allowedExtensions = new[] { ".csv", ".json", ".xml" };

    var fileUtility = new FileUtility(new FileSystem());
    var files = fileUtility.GetAllFiles(dataDirectory, allowedExtensions);

    var foundResults = new List<string>();

    foreach (var file in files)
    {
        fileUtility.GetExtension(file);
        var content = fileUtility.GetContent(file);
        IEnumerable<Data> results = Enumerable.Empty<Data>();

        if (fileUtility.GetExtension(file) == ".csv")
        {
            results = new CsvContentParser().Parse(content);
        }
        else if (fileUtility.GetExtension(file) == ".json")
        {
            results = new JsonContentParser().Parse(content);
        }
        else if (fileUtility.GetExtension(file) == ".xml")
        {
            results = new XmlContentParser().Parse(content);
        }
        var matches = results
            .Where(data => data.Key.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase))
            .ToList();

        foreach (var match in matches)
        {
            foundResults.Add($"Key:{match.Key} Value:{match.Value} FileName:{file}");
        }
    }
    if (foundResults.Any())
    {
        foundResults.ForEach(Console.WriteLine);
    }
    else
    {
        Console.WriteLine("No results matched your search.");
    }
}