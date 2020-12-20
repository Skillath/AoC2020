using System.Linq;

namespace AoC2020.AoC2020_4.Validator
{
    public readonly struct EyeColorValidator : IValidator
    {
        private static readonly string[] ValidEyeColors =
        {
            "amb", "blu", "brn", "gry", "grn", "hzl", "oth",
        };

        public bool Validate(string value)
        {
            return ValidEyeColors.Any(color => color == value);
        }
    }
}