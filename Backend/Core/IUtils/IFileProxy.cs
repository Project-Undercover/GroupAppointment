using static Infrastructure.Enums.Enums;

namespace Core.IUtils
{
    public interface IFileProxy
    {
        Task<string> SaveFile(string fileName, Folder folder = Folder.Default);
    }
}
