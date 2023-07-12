namespace Sparky;

public class CustomerXUnitTests
{
    private Customer customer;
	public CustomerXUnitTests()
	{
		customer = new();
	}

    [Fact]
    public void CombineName_InputFirstAndLastName_ReturnFullName()
	{
		//Arrange
		string firstName = "Ariel";
		string lastName = "Vieira";

		//Act
		customer.GreetAndCombineNames(firstName, lastName);

		//Assert
		Assert.Equal("Hello, Ariel Vieira", customer.GreetMessage);
		Assert.Contains("Ariel Vieira", customer.GreetMessage);
		Assert.StartsWith("Hello,", customer.GreetMessage);
		Assert.EndsWith("Vieira", customer.GreetMessage);
		Assert.Matches("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", customer.GreetMessage);
	}

    [Fact]
    public void GreetMessage_NotGreeted_ReturnsNull()
    {
        //Arrange

        //Act

        //Assert
        Assert.Null(customer.GreetMessage);
    }

    [Fact]
    public void DiscountCheck_DefaultCustomer_ReturnsDiscountRange()
    {
        //Arrange

        //Act
        int result = customer.Discount;

        //Assert
        Assert.InRange(result, 10, 25);
    }

    [Fact]
    public void GreetMessage_GreetedWithoutLastName_ReturnsNotNull()
    {
        //Act
        customer.GreetAndCombineNames("Ariel", "");

        //Assert
        Assert.NotNull(customer.GreetMessage);
        Assert.False(string.IsNullOrEmpty(customer.GreetMessage));
    }

    [Fact]
    public void GreetChecker_EmptyFirstName_ThrowsException()
    {
        //Assert
        var exceptionDetails = Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", "Vieira"));
        Assert.Equal("Empty First Name", exceptionDetails.Message);
        Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", "Vieira"));
  
    }

    [Fact]
    public void CustomerType_CreateCustomerWithLessThan100Order_ReturnsBasicCustomer()
    {
        //Arrange
        customer.OrderTotal = 10;

        //Act
        var result = customer.GetCustomerDetails();

        //Assert
        Assert.IsType<BasicCustomer>(result);
    }

    [Fact]
    public void CustomerType_CreateCustomerWithMoreThan100Order_ReturnsBasicCustomer()
    {
        //Arrange
        customer.OrderTotal = 101;

        //Act
        var result = customer.GetCustomerDetails();

        //Assert
        Assert.IsType<PlatinumCustomer>(result);
    }
}
