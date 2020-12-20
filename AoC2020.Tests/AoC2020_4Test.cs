using System.Collections.Generic;
using NUnit.Framework;
using AoC2020.AoC2020_4.Validator;

namespace AoC2020.Tests
{
    public class AoC2020_4Test
    {
        private static readonly IEnumerable<AoC2020_4Puzzle.PassportEntryData> PassportEntries = new[]
        {
            new AoC2020_4Puzzle.PassportEntryData("byr","Birth Year", validator: new DateValidator(1920, 2002)),
            new AoC2020_4Puzzle.PassportEntryData("iyr","Issue Year", validator: new DateValidator(2010, 2020)),
            new AoC2020_4Puzzle.PassportEntryData("eyr","Expiration Year", validator: new DateValidator(2020, 2030)),
            new AoC2020_4Puzzle.PassportEntryData("hgt","Height", validator: new HeightValidator()),
            new AoC2020_4Puzzle.PassportEntryData("hcl","Hair Color", validator: new HairColorValidator()),
            new AoC2020_4Puzzle.PassportEntryData("ecl","Eye Color", validator: new EyeColorValidator()),
            new AoC2020_4Puzzle.PassportEntryData("pid","Passport ID", validator: new PassportIdValidator()),
            new AoC2020_4Puzzle.PassportEntryData("cid","Country ID", false),
        };


        [SetUp]
        public void Setup()
        {

        }

        [TestCase(1920, 2003, 1800, ExpectedResult = false)]
        [TestCase(1920, 2002, 2001, ExpectedResult = true)]
        [TestCase(2010, 2020, 2010, ExpectedResult = true)]
        [TestCase(2020, 2029, 2030, ExpectedResult = false)]
        [TestCase(10000, 200000, 50000, ExpectedResult = false)]
        public bool Test_DateValidator(int minDate, int maxDate, int value)
        {
            var validator = new DateValidator(minDate, maxDate);
            return validator.Validate(value.ToString());
        }

        [TestCase("150cm", ExpectedResult = true)]
        [TestCase("193cm", ExpectedResult = true)]
        [TestCase("179cm", ExpectedResult = true)]
        [TestCase("1729cm", ExpectedResult = false)]
        [TestCase("172mm", ExpectedResult = false)]
        [TestCase("59in", ExpectedResult = true)]
        [TestCase("76in", ExpectedResult = true)]
        [TestCase("86in", ExpectedResult = false)]
        [TestCase("36in", ExpectedResult = false)]
        [TestCase("76ln", ExpectedResult = false)]
        public bool Test_HeightValidator(string value)
        {
            var validator = new HeightValidator();
            return validator.Validate(value);
        }

        [TestCase("#FFFFFF", ExpectedResult = true)]
        [TestCase("#000000", ExpectedResult = true)]
        [TestCase("#12345F", ExpectedResult = true)]
        [TestCase("#12345", ExpectedResult = false)]
        [TestCase("1112345", ExpectedResult = false)]
        [TestCase("#GGGGGG", ExpectedResult = false)]
        [TestCase("#123", ExpectedResult = false)]
        [TestCase("#123123132", ExpectedResult = false)]
        public bool Test_HairColorValidator(string value)
        {
            var validator = new HairColorValidator();
            return validator.Validate(value);
        }

        [TestCase("amb", ExpectedResult = true)]
        [TestCase("blu", ExpectedResult = true)]
        [TestCase("brn", ExpectedResult = true)]
        [TestCase("gry", ExpectedResult = true)]
        [TestCase("grn", ExpectedResult = true)]
        [TestCase("hzl", ExpectedResult = true)]
        [TestCase("oth", ExpectedResult = true)]
        [TestCase("aaaa", ExpectedResult = false)]
        [TestCase("123", ExpectedResult = false)]
        public bool Test_EyeColorValidator(string value)
        {
            var validator = new EyeColorValidator();
            return validator.Validate(value);
        }

        [TestCase("000000000", ExpectedResult = true)]
        [TestCase("123456789", ExpectedResult = true)]
        [TestCase("000000789", ExpectedResult = true)]
        [TestCase("1234567891", ExpectedResult = false)]
        [TestCase("1234567", ExpectedResult = false)]
        [TestCase("11a4567", ExpectedResult = false)]
        public bool Test_PassportIdValidator(string value)
        {
            var validator = new PassportIdValidator();
            return validator.Validate(value);
        }
    }
}