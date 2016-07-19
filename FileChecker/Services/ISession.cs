using FileChecker.Entities;

namespace FileChecker.Services
{
    public interface ISession
    {
        ComparisonSettings Settings { get; set; }
    }
}