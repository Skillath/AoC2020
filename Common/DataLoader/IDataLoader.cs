using System.Threading;
using System.Threading.Tasks;

namespace AoC.Common.DataLoader
{
    public interface IDataLoader
    {
        Task<string> Load(int day, CancellationToken cancellationToken = default);
    }
}
