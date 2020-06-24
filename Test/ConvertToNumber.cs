using System;
using Xunit;
using Library;
using FluentAssertions;

namespace Test
{
    public class ConvertToNumber
    {
        [Fact]
        public void LongIntegerTest()
        {
            var expectedValue = Int64.MaxValue;

            var str = expectedValue.ToString();

            var converted = Questions.ConvertToNumber(str);
            converted.Should().BeFalse("the sample code can't handle big integers");

            converted = Questions.TryConvertToNumber<Int64>(str, out var value);
            converted.Should().BeTrue("the new code should handle a large integer");
            value.Should().Be(expectedValue);
        }

        [Fact]
        public void FloatTest()
        {
            float expectedValue = 3.14159F;

            var str = expectedValue.ToString();
            
            var converted = Questions.ConvertToNumber(str);
            converted.Should().BeFalse("the sample code can't handle floats");

            converted = Questions.TryConvertToNumber<float>(str, out var value);
            converted.Should().BeTrue("the new code should handle a float");
            value.Should().Be(expectedValue);
        }
        
        [Fact]
        public void ParseZeroTest() 
        {
            var expectedValue = 0;

            var str = expectedValue.ToString();

            var converted = Questions.ConvertToNumber(str);
            converted.Should().BeFalse("the sample code can't parse zero");

            converted = Questions.TryConvertToNumber<int>(str, out var value);
            converted.Should().BeTrue("the new code should handle a zero");
            value.Should().Be(expectedValue);
        }
    }
}
