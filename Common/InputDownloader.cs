using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AoC.Common
{
    public interface IInputLoader
    {
        Task<string> Load(int day);
    }

    public class InputDownloader : IInputLoader
    {
        private const string Url = @"https://adventofcode.com/2020/day/{0}/input";
        private const string FileName = "input_{0}";


        public async Task<string> Load(int day)
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

    public class InputLoaderFromDisk : IInputLoader
    {
        private const string FileName = "input_{0}";

        private readonly string _path;

        public InputLoaderFromDisk(string path = "")
        {
            _path = path;
        }

        public async Task<string> Load(int day)
        {
            var fileName = Path.Combine(_path, string.Format(FileName, day));

            using var streamReader = new StreamReader(fileName);
            return await streamReader.ReadToEndAsync();
        }
    }
}
