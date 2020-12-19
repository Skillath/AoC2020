using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AoC2020
{
    /*
    https://adventofcode.com/2020/day/4

    byr (Birth Year)
    iyr (Issue Year)
    eyr (Expiration Year)
    hgt (Height)
    hcl (Hair Color)
    ecl (Eye Color)
    pid (Passport ID)
    cid (Country ID)

    Passport data is validated in batch files (your puzzle input). Each passport is represented as a sequence of key:value pairs separated by spaces or newlines. 
    Passports are separated by blank lines.

    */
    public class AoC2020_4 : PuzzleBase<IEnumerable<AoC2020_4.Passport>, int>
    {
        public interface IValidator
        {
            bool Validate(string value);
        }

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
                       && number < _maxDate;
            }
        }

        public readonly struct HeightValidator : IValidator
        {
            private const string HeightValidatorPattern = @"^\d*(cm|in)$";
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

                var splittedValue = Regex.Split(value);
                if (!int.TryParse(splittedValue.FirstOrDefault(), out var number))
                    return false;

                var (min, max) = Ranges[splittedValue.Last()];

                return number >= min && number <= max;
            }
        }

        public readonly struct HairColorValidator : IValidator
        {
            private const string HairColorValidatorPattern = @"^#[0-9a-fA-F]{6}$";
            private static readonly Regex Regex = new Regex(HairColorValidatorPattern);
            public bool Validate(string value)
            {
                return value.Length <= 7 && Regex.IsMatch(value);
            }
        }

        public readonly struct EyeColorValidator : IValidator
        {
            private static readonly string[] ValidEyeColors = new[]
            {
                "amb", "blu", "brn", "gry", "grn", "hzl", "oth",
            };

            public bool Validate(string value)
            {
                return ValidEyeColors.Any(color => color == value);
            }
        }

        public readonly struct PassportIdValidator : IValidator
        {
            private const string PassportIdValidationPattern = @"^[0-9]{9}$";
            private static readonly Regex Regex = new Regex(PassportIdValidationPattern);

            public bool Validate(string value)
            {
                return Regex.IsMatch(value);
            }
        }

        public readonly struct PassportEntryData
        {
            public string Id { get; }
            public string Name { get; }
            public bool IsRequired { get; }
            public IValidator Validator { get; }

            public PassportEntryData(string id, string name, bool isRequired = true, IValidator validator = null)
            {
                Id = id;
                Name = name;
                IsRequired = isRequired;
                Validator = validator;
            }
        }

        public readonly struct PassportEntry
        {
            public PassportEntryData Id { get; }
            public string Value { get; }

            public PassportEntry(PassportEntryData id, string value)
            {
                Id = id;
                Value = value;
            }
        }

        public readonly struct Passport
        {
            public IEnumerable<PassportEntry> Entries { get; }

            public Passport(IEnumerable<PassportEntry> entries)
            {
                Entries = entries;
            }
        }

        private static readonly IEnumerable<PassportEntryData> PassportEntries = new[]
        {
            new PassportEntryData("byr","Birth Year", validator: new DateValidator(1920, 2002)),
            new PassportEntryData("iyr","Issue Year", validator: new DateValidator(2010, 2020)),
            new PassportEntryData("eyr","Expiration Year", validator: new DateValidator(2020, 2030)),
            new PassportEntryData("hgt","Height", validator: new HeightValidator()),
            new PassportEntryData("hcl","Hair Color", validator: new HairColorValidator()),
            new PassportEntryData("ecl","Eye Color", validator: new EyeColorValidator()),
            new PassportEntryData("pid","Passport ID", validator: new PassportIdValidator()),
            new PassportEntryData("cid","Country ID", false),
        };

        protected override int Day => 4;

        protected override async ValueTask<IEnumerable<Passport>> ParseLoadedData(string loadedData, CancellationToken cancellationToken = default)
        {
            var data = loadedData.Split(Environment.NewLine + Environment.NewLine)
                .ToAsyncEnumerable();

            return await data.SelectAwaitWithCancellation(ParsePassport)
                .ToArrayAsync(cancellationToken);
        }

        protected override async ValueTask<int> FirstPart(IEnumerable<Passport> input, CancellationToken cancellationToken = default)
        {
            return await input.ToAsyncEnumerable()
                .CountAsync(p => p.Entries.All(entry => PassportEntries.Contains(entry.Id) &&
                                (!entry.Id.IsRequired || !string.IsNullOrEmpty(entry.Value))), cancellationToken);
        }

        /*
        byr (Birth Year) - four digits; at least 1920 and at most 2002.
        iyr (Issue Year) - four digits; at least 2010 and at most 2020.
        eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
        hgt (Height) - a number followed by either cm or in:
        If cm, the number must be at least 150 and at most 193.
        If in, the number must be at least 59 and at most 76.
        hcl (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
        ecl (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
        pid (Passport ID) - a nine-digit number, including leading zeroes.
        cid (Country ID) - ignored, missing or not.
         */
        protected override async ValueTask<int> SecondPart(IEnumerable<Passport> input, CancellationToken cancellationToken = default)
        {
            return await input.ToAsyncEnumerable()
                .CountAsync(p => p.Entries.All(entry => PassportEntries.Contains(entry.Id) &&
                               (!entry.Id.IsRequired || !string.IsNullOrEmpty(entry.Value)) &&
                               (entry.Id.Validator?.Validate(entry.Value) ?? true)), cancellationToken);
        }

        /*
        EXAMPLE:

        ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
        byr:1937 iyr:2017 cid:147 hgt:183cm

        iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
        hcl:#cfa07d byr:1929

        hcl:#ae17e1 iyr:2013
        eyr:2024
        ecl:brn pid:760753108 byr:1931
        hgt:179cm

        hcl:#cfa07d eyr:2025 pid:166559648
        iyr:2011 ecl:brn hgt:59in
        */

        private static async ValueTask<Passport> ParsePassport(string rawPassport, CancellationToken cancellationToken = default)
        {
            var pattern = @"[a-z]*:[#\w]*";
            var regex = new Regex(pattern);

            var data = await regex.Matches(rawPassport)
                .ToAsyncEnumerable()
                .Select(d =>
                {
                    var splittedData = d.Value.Split(':');
                    return (Id: splittedData.First(), Value: splittedData.Last());
                })
                .ToArrayAsync(cancellationToken);

            var passportEntries = await PassportEntries
                .ToAsyncEnumerable()
                .Select(entry =>
                {
                    var value = data.FirstOrDefault(d => d.Id == entry.Id);
                    return new PassportEntry(entry, value.Value);
                })
                .ToArrayAsync(cancellationToken);

            return new Passport(passportEntries);
        }
    }
}