using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AoC.Common;
using Common;

namespace AoC2020
{
    //https://adventofcode.com/2020/day/2
    /*
     For example, suppose you have the following list:

    1-3 a: abcde
    1-3 b: cdefg
    2-9 c: ccccccccc
    Each line gives the password policy and then the password. The password policy indicates the lowest and highest number of times a given letter must appear for the password to be valid. For example, 1-3 a means that the password must contain a at least 1 time and at most 3 times.

    In the above example, 2 passwords are valid. The middle password, cdefg, is not; it contains no instances of b, but needs at least 1. The first and third passwords are valid: they contain one a or nine c, both within the limits of their respective policies.

    How many passwords are valid according to their policies?
     */
    public class AoC2020_2 : PuzzleBase
    {
        private struct PasswordPolicy
        {
            public int Min { get; set; }
            public int Max { get; set; }
            public char Letter { get; set; }
        }

        protected override int Day => 2;

        public override async Task<string> Resolve(string input, CancellationToken cancellationToken = default)
        {
            var loadedData = (await InputLoader.Load(Day, cancellationToken).ConfigureAwait(false))
                .Replace("\r", string.Empty)
                .Normalize()
                .Split('\n')
                .Select(Parse)
                .ToArray();

            var partOne = loadedData.Count(data => IsPasswordValidPartOne(data.Policy, data.Password));
            var partTwo = loadedData.Count(data => IsPasswordValidPartTwo(data.Policy, data.Password));

            return $"Puzzle {Day}\n{partOne}\n{partTwo}";
        }

        /*
         *1-3 a: abcde
         *1-3 b: cdefg
         *2-9 c: ccccccccc
         */
        private (PasswordPolicy Policy, string Password) Parse(string rawPolicy)
        {
            var splittedPass = rawPolicy
                .Replace('-', ' ')
                .Replace(":", string.Empty)
                .Split(' ');

            if (splittedPass.Length > 4)
                throw new Exception("Parsing Error");


            var policy = new PasswordPolicy
            {
                Min = int.Parse(splittedPass[0]),
                Max = int.Parse(splittedPass[1]),
                Letter = char.Parse(splittedPass[2])
            };

            return (policy, splittedPass.Last());
        }

        private bool IsPasswordValidPartOne(PasswordPolicy policy, string password)
        {
            var characterCount = password.Count(c => c == policy.Letter);
            return characterCount >= policy.Min && characterCount <= policy.Max;
        }

        private bool IsPasswordValidPartTwo(PasswordPolicy policy, string password)
        {
            return password.Contains(policy.Letter) &&
                   (password.ElementAtOrDefault(policy.Min - 1) == policy.Letter &&
                    password.ElementAtOrDefault(policy.Max - 1) != policy.Letter) ||
                   (password.ElementAtOrDefault(policy.Min - 1) != policy.Letter &&
                    password.ElementAtOrDefault(policy.Max - 1) == policy.Letter);
        }
    }

    /*
     * https://adventofcode.com/2020/day/3
     */
}