using Xunit;
using Library;
using FluentAssertions;

namespace Test
{
    public class GenerateLowestNumber
    {
        [Theory]
        [InlineData("543210", 2, "3210")]
        [InlineData("4205123", 4, "012")]
        [InlineData("4205186", 4, "016")]
        [InlineData("00112233445566778899", 15, "00112")]
        [InlineData("999888777666555444333222111000", 25, "11000")]
        [InlineData("99988877766655544433322211100000112233445566778899", 44, "000001")]
        public void GenerateLowestNumberTest(string number, int n, string expected)
        {
            var result = Questions.GenerateLowestNumber(number, n);
            result.Should().Be(expected);
        }
    }
}