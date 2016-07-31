using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace Library10.Core.IO
{
    public interface IStorageService
    {
        Task<bool> ExistsFile(string filePath, StorageStrategy strategy = StorageStrategy.Local);

        Task<StorageFile> CreateFile(string filePath, byte[] stream, StorageStrategy strategy = StorageStrategy.Local, CreationCollisionOption options = CreationCollisionOption.FailIfExists);

        Task<StorageFile> CreateFile(string filePath, StorageStrategy strategy = StorageStrategy.Local, CreationCollisionOption options = CreationCollisionOption.FailIfExists);

        Task<Stream> OpenFile(string filePath, StorageStrategy strategy = StorageStrategy.Local);

        void DeleteFile(string filePath, StorageStrategy strategy = StorageStrategy.Local);

        Task<string[]> ListFiles(string path, StorageStrategy strategy = StorageStrategy.Local);

        Task<StorageFolder> CreateDirectories(string path, StorageStrategy strategy);

        Task<StorageFolder> OpenDirectory(string path, StorageStrategy strategy);
    }
}