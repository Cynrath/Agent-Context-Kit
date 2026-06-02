namespace AgentContextKit.Core;

public sealed class PhysicalFileSystem : IFileSystem
{
    public bool FileExists(string path)
    {
        return File.Exists(path);
    }

    public bool DirectoryExists(string path)
    {
        return Directory.Exists(path);
    }

    public void CreateDirectory(string path)
    {
        if (!string.IsNullOrWhiteSpace(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    public string ReadAllText(string path)
    {
        return File.ReadAllText(path);
    }

    public void WriteAllText(string path, string content)
    {
        var directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        File.WriteAllText(path, content);
    }

    public long GetFileLength(string path)
    {
        return new FileInfo(path).Length;
    }

    public IEnumerable<string> EnumerateFiles(string rootPath, IReadOnlySet<string> ignoredDirectoryNames)
    {
        if (!Directory.Exists(rootPath))
        {
            yield break;
        }

        var pending = new Stack<string>();
        pending.Push(rootPath);

        while (pending.Count > 0)
        {
            var current = pending.Pop();
            IEnumerable<string> directories;
            IEnumerable<string> files;

            try
            {
                directories = Directory.EnumerateDirectories(current);
                files = Directory.EnumerateFiles(current);
            }
            catch (UnauthorizedAccessException)
            {
                continue;
            }
            catch (IOException)
            {
                continue;
            }

            foreach (var directory in directories)
            {
                var name = Path.GetFileName(directory);
                if (!ignoredDirectoryNames.Contains(name))
                {
                    pending.Push(directory);
                }
            }

            foreach (var file in files)
            {
                yield return file;
            }
        }
    }
}

public sealed class SystemClock : IClock
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
