using System.Threading;
using System.Threading.Tasks;

namespace AoC.Common.Puzzle
{
    public interface IPuzzle
    {
        ValueTask<string> Resolve(CancellationToken cancellationToken = default);
    }
}