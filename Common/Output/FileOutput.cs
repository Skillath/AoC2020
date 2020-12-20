using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AoC.Common.Output
{
    public class FileOutput : IOutput
    {
        private readonly string _path;

        public FileOutput(string path, string filename)
        {
            _path = Path.Combine(path, filename);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            ClearOutput();
        }

        private void ClearOutput()
        {
            File.WriteAllText(_path, string.Empty);
        }

        public async ValueTask WriteAsync(object output, CancellationToken cancellationToken = default)
        {
            await File.AppendAllTextAsync(_path, output + "\n", cancellationToken);
        }
    }
}
