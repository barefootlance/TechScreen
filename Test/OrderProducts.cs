using Xunit;
using Library;
using System.Collections.Generic;
using FluentAssertions;

namespace Test
{
    public class OrderProducts
    {
        [Fact]
        public void OrderProductsTest()
        {
            var productCodes = new string[] 
            {
                "Can",
                "1",
                "Be",
                "Sure",
                "2",
                "Output",
                "a",
                "List",
                "In",
                "Order"
            };

            var expectedHigh = new HashSet<string>(){"Output", "a", "In", "Order"};
            var expectedMedium = new HashSet<string>(){"1", "2"};

            var questions = new Questions();
            questions.OrderProductsByPriority(productCodes);

            var currentPriority = 1; // expect to start with high
            foreach (var productCode in productCodes)
            {
                // move to the next priority if we've hit a boundary
                currentPriority = currentPriority switch
                {
                    1 when !expectedHigh.Contains(productCode) => 2,
                    2 when !expectedMedium.Contains(productCode) => 3,
                    _ => currentPriority
                };

                // and make sure that the priorities are what we expected
                switch (currentPriority)
                {
                    case 1: 
                        expectedHigh.Should().Contain(productCode);
                        expectedMedium.Should().NotContain(productCode);
                        break;
                    
                    case 2:
                        expectedHigh.Should().NotContain(productCode);
                        expectedMedium.Should().Contain(productCode);
                        break;

                    default:
                        expectedHigh.Should().NotContain(productCode);
                        expectedMedium.Should().NotContain(productCode);
                        break;
                }
            }
        }
    }
}