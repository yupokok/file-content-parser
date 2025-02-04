﻿using System.IO.Abstractions;
using CodingAssignmentLib.Abstractions;

namespace CodingAssignmentLib;

public class FileUtility : IFileUtility
{
    private readonly IFileSystem _fileSystem;

    public FileUtility(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }
    
    public string GetExtension(string fileName)
    {
        return _fileSystem.FileInfo.New(fileName).Extension;
    }

    public string GetContent(string fileName)
    {
        return _fileSystem.File.ReadAllText(fileName);
    }

    public IEnumerable<string> GetAllFiles(string directoryPath, string[] allowedExtensions)
    {
        var allFiles = _fileSystem.Directory.GetFiles(directoryPath, "*.*", System.IO.SearchOption.AllDirectories);
        return allFiles.Where(file => allowedExtensions.Contains(GetExtension(file)));

    }
}