using System;
using System.Threading;
using System.Threading.Tasks;

namespace AoC.Common.Output
{
    public class ConsoleOutput : IOutput
    {
        public async ValueTask WriteAsync(object output, CancellationToken cancellationToken = default)
        {
            Console.WriteLine(output);
        }
    }
}