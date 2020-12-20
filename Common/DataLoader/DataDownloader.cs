using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AoC.Common.DataLoader
{
    public class DataDownloader : IDataLoader
    {
        private const string Url = @"https://adventofcode.com/2020/day/{0}/input";
        private const string FileName = "input_{0}";

        public async Task<string> Load(int day, CancellationToken cancellationToken = default)
        {
            var url = string.Format(Url, day);
            var fileName = string.Format(FileName, day);

            using var webClient = new WebClient
            {
                Credentials = new CredentialCache()
                {

                }
            };

            await webClient.DownloadFileTaskAsync(url, fileName);


            return "";
        }
    }
}