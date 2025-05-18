namespace LogAnalysis.Models
{
    public interface IFileReaderModel
    {
        bool Exists(string path);
        string[] ReadAllLines(string path);
    }
}
