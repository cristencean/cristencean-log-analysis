namespace LogAnalysis.Models
{
    public class FileReaderModel : IFileReaderModel
    {
        public bool Exists(string path) => File.Exists(path);
        public string[] ReadAllLines(string path) => File.ReadAllLines(path);
    }
}
