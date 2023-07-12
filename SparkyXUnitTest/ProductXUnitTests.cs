namespace Sparky;

public class ProductXUnitTests
{
    [Fact]
    public void GetProductPrice_PlatinumCustomer_ReturnsPriceWith20Discount()
    {
        //Arrange
        Product product = new() { Price = 50 };

        //Act
        var result = product.GetPrice(new Customer() { IsPlatinum = true });

        //Assert
        Assert.Equal(40, result);
    }
}
