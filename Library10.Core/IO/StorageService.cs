using Library10.Common.Extensions;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace Library10.Core.IO
{
    public class StorageService : IStorageService
    {
        private StorageFolder Container(StorageStrategy strategy)
        {
            switch (strategy)
            {
                case StorageStrategy.Local:
                    return ApplicationData.Current.LocalFolder;

                case StorageStrategy.Roaming:
                    return ApplicationData.Current.RoamingFolder;

                case StorageStrategy.Temporary:
                    return ApplicationData.Current.TemporaryFolder;

                default:
                    throw new NotSupportedException("Not Supported");
            }
        }

        async public Task<bool> ExistsFile(string filePath, StorageStrategy strategy = StorageStrategy.Local)
        {
            try
            {
                var currentFolder = await CreateDirectories(filePath, strategy);
                var filename = filePath.PathToFileName();

                await currentFolder.GetFileAsync(filename);
                return true;
            }
            catch
            {
                return false;
            }
        }

        async public Task<StorageFile> CreateFile(string filePath, byte[] stream, StorageStrategy strategy = StorageStrategy.Local, CreationCollisionOption options = CreationCollisionOption.FailIfExists)
        {
            var currentFolder = await CreateDirectories(filePath, strategy);
            var filename = filePath.PathToFileName();

            var file = await currentFolder.CreateFileAsync(filename, options);

            using (var writeStream = await file.OpenStreamForWriteAsync())
            {
                writeStream.Write(stream, 0, stream.Length);
            }

            return file;
        }

        async public Task<StorageFile> CreateFile(string filePath, StorageStrategy strategy = StorageStrategy.Local, CreationCollisionOption options = CreationCollisionOption.FailIfExists)
        {
            var currentFolder = await CreateDirectories(filePath, strategy);
            var filename = filePath.PathToFileName();

            return await currentFolder.CreateFileAsync(filename, options);
        }

        async public Task<Stream> OpenFile(string filePath, StorageStrategy strategy = StorageStrategy.Local)
        {
            var currentFolder = await OpenDirectory(filePath, strategy);
            var filename = filePath.PathToFileName();

            return await currentFolder.OpenStreamForReadAsync(filename);
        }

        async public void DeleteFile(string filePath, StorageStrategy strategy = StorageStrategy.Local)
        {
            var currentFolder = await OpenDirectory(filePath, strategy);
            var filename = filePath.PathToFileName();

            var file = await currentFolder.GetFileAsync(filename);

            await file.DeleteAsync(StorageDeleteOption.PermanentDelete);
        }

        async public Task<string[]> ListFiles(string path, StorageStrategy strategy = StorageStrategy.Local)
        {
            var currentFolder = await OpenDirectory(path, strategy);

            var list = await currentFolder.GetFilesAsync();

            var result = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
                result[i] = list[i].Name;

            return result;
        }

        async public Task<StorageFolder> CreateDirectories(string path, StorageStrategy strategy) // StorageStrategy.Local)
        {
            var directories = path.PathToDirectories();

            var currentFolder = Container(strategy);
            foreach (var directory in directories)
                if (!string.IsNullOrEmpty(directory))
                    currentFolder = await currentFolder.CreateFolderAsync(directory, CreationCollisionOption.OpenIfExists);

            return currentFolder;
        }

        async public Task<StorageFolder> OpenDirectory(string path, StorageStrategy strategy) // StorageStrategy.Local)
        {
            var directories = path.PathToDirectories();

            var currentFolder = Container(strategy);
            foreach (var directory in directories)
                if (!string.IsNullOrEmpty(directory))
                {
                    var tmp = await currentFolder.GetFolderAsync(directory);
                    currentFolder = null;
                    currentFolder = tmp;
                }
            return currentFolder;
        }
    }
}