using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TextCopy;

namespace AoC.Common
{
    public interface IOutput
    {
        ValueTask WriteAsync(object output, CancellationToken cancellationToken = default);
    }

    public class ConsoleOutput : IOutput
    {
        public async ValueTask WriteAsync(object output, CancellationToken cancellationToken = default)
        {
            Console.WriteLine(output);
        }
    }

    public class ClipboardOutput : IOutput
    {
        public async ValueTask WriteAsync(object output, CancellationToken cancellationToken = default)
        {
            await ClipboardService.SetTextAsync(string.Empty, cancellationToken).ConfigureAwait(false);
            await ClipboardService.SetTextAsync(output.ToString(), cancellationToken);
        }
    }

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
