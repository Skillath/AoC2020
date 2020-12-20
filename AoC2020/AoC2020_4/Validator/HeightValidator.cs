using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2020.AoC2020_4.Validator
{
    public readonly struct HeightValidator : IValidator
    {
        private const string HeightValidatorPattern = @"(\d*)(cm|in)";
        private static readonly Regex Regex = new Regex(HeightValidatorPattern);

        private static readonly IReadOnlyDictionary<string, (int Min, int Max)> Ranges =
            new Dictionary<string, (int Min, int Max)>()
            {
                {"in", (59, 76)},
                {"cm", (150, 193)},
            };


        public bool Validate(string value)
        {
            if (!Regex.IsMatch(value))
                return false;

            var splittedValue = Regex.Split(value)
                .Where(v => v != string.Empty)
                .ToArray();
            if (!int.TryParse(splittedValue.FirstOrDefault(), out var number))
                return false;

            var (min, max) = Ranges[splittedValue.Last()];

            return number >= min && number <= max;
        }
    }
}