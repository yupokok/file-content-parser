using CodingAssignmentLib;
using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentTests;

public class JsonContentParserTests
{
    private JsonContentParser _sut = null!;

    [SetUp]
    public void Setup()
    {
        _sut = new JsonContentParser();
    }

    [Test]
    public void Parse_ReturnsData_FromValidJson()
    {
        var content = @"[
            { ""Key"": ""a"", ""Value"": ""b"" },
            { ""Key"": ""c"", ""Value"": ""d"" }
        ]";

        var dataList = _sut.Parse(content).ToList();

        Assert.That(dataList, Is.EqualTo(new List<Data>
        {
            new("a", "b"),
            new("c", "d")
        }).AsCollection);
    }
}