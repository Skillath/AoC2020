using System.Text.RegularExpressions;

namespace AoC2020.AoC2020_4.Validator
{
    public readonly struct DateValidator : IValidator
    {
        private const string YearPattern = @"^\d{4}$";
        private static readonly Regex Regex = new Regex(YearPattern);

        private readonly int _minDate;
        private readonly int _maxDate;

        public DateValidator(int minDate, int maxDate)
        {
            _maxDate = maxDate;
            _minDate = minDate;
        }

        public bool Validate(string value)
        {
            return Regex.IsMatch(value)
                   && int.TryParse(value, out var number)
                   && number >= _minDate
                   && number <= _maxDate;
        }
    }
}