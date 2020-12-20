using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AoC.Common.DataLoader
{
    public class DataLoaderFromDisk : IDataLoader
    {
        private const string FileName = "input_{0}";

        private readonly string _path;

        public DataLoaderFromDisk(string path = "")
        {
            _path = path;
        }

        public async Task<string> Load(int day, CancellationToken cancellationToken = default)
        {
            var fileName = Path.Combine(_path, string.Format(FileName, day));

            using var streamReader = new StreamReader(fileName);
            return await streamReader.ReadToEndAsync();
        }
    }
}