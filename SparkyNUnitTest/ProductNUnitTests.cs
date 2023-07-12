using NUnit.Framework;

namespace Sparky;

[TestFixture]
public class ProductNUnitTests
{
    [Test]
    public void GetProductPrice_PlatinumCustomer_ReturnsPriceWith20Discount()
    {
        //Arrange
        Product product = new Product() { Price = 50 };

        //Act
        var result = product.GetPrice(new Customer() { IsPlatinum = true });

        //Assert
        Assert.That(result, Is.EqualTo(40));
    }
}
