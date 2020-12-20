using System.Text.RegularExpressions;

namespace AoC2020.AoC2020_4.Validator
{
    public readonly struct HairColorValidator : IValidator
    {
        private const string HairColorValidatorPattern = @"^#[0-9a-fA-F]{6}$";
        private static readonly Regex Regex = new Regex(HairColorValidatorPattern);
        public bool Validate(string value)
        {
            return value.Length <= 7 && Regex.IsMatch(value);
        }
    }
}