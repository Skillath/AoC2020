using System.Text.RegularExpressions;

namespace AoC2020.AoC2020_4.Validator
{
    public readonly struct PassportIdValidator : IValidator
    {
        private const string PassportIdValidationPattern = @"^[0-9]{9}$";
        private static readonly Regex Regex = new Regex(PassportIdValidationPattern);

        public bool Validate(string value)
        {
            return Regex.IsMatch(value);
        }
    }
}