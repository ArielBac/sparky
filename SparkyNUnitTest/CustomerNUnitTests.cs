using NUnit.Framework;

namespace Sparky;

[TestFixture]
public class CustomerNUnitTests
{
    private Customer customer;
    [SetUp]
    public void Setup()
    {
        customer = new Customer();
    }

    [Test]
    public void CombineName_InputFirstAndLastName_ReturnFullName()
    {
        //Arrange

        //Act
        customer.GreetAndCombineNames("Ariel", "Vieira");

        //Assert
        Assert.Multiple(() => 
        {
            Assert.AreEqual(customer.GreetMessage, "Hello, Ariel Vieira");
            Assert.That(customer.GreetMessage, Is.EqualTo("Hello, Ariel Vieira"));
            Assert.That(customer.GreetMessage, Does.Contain("ariel vieira").IgnoreCase); //Ignora case sensitive
            Assert.That(customer.GreetMessage, Does.StartWith("Hello,"));
            Assert.That(customer.GreetMessage, Does.EndWith("Vieira"));
            Assert.That(customer.GreetMessage, Does.Match("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+"));
        });
    }

    [Test]
    public void GreetMessage_NotGreeted_ReturnsNull()
    {
        //Arrange
        
        //Act

        //Assert
        Assert.IsNull(customer.GreetMessage);
    }

    [Test]
    public void DiscountCheck_DefaultCustomer_ReturnsDiscountRange()
    {
        //Arrange

        //Act
        int result = customer.Discount;

        //Assert
        Assert.That(result, Is.InRange(10, 25));
    }

    [Test]
    public void GreetMessage_GreetedWithoutLastName_ReturnsNotNull()
    {
        //Act
        customer.GreetAndCombineNames("Ariel", "");

        //Assert
        Assert.IsNotNull(customer.GreetMessage);
        Assert.IsFalse(string.IsNullOrEmpty(customer.GreetMessage));
    }

    [Test] 
    public void GreetChecker_EmptyFirstName_ThrowsException()
    {
        //Assert
        var exceptionDetails = Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", "Vieira"));
        Assert.AreEqual("Empty First Name", exceptionDetails.Message);

        Assert.That(() => customer.GreetAndCombineNames("", "Ariel"), 
            Throws.ArgumentException.With.Message.EqualTo("Empty First Name"));

        Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", "Vieira"));
        Assert.That(() => customer.GreetAndCombineNames("", "Ariel"),
            Throws.ArgumentException.With.Message.EqualTo("Empty First Name"));
    }

    [Test]
    public void CustomerType_CreateCustomerWithLessThan100Order_ReturnsBasicCustomer()
    {
        //Arrange
        customer.OrderTotal = 10;
        
        //Act
        var result = customer.GetCustomerDetails();

        //Assert
        Assert.That(result, Is.TypeOf<BasicCustomer>());
    }

    [Test]
    public void CustomerType_CreateCustomerWithMoreThan100Order_ReturnsBasicCustomer()
    {
        //Arrange
        customer.OrderTotal = 101;

        //Act
        var result = customer.GetCustomerDetails();

        //Assert
        Assert.That(result, Is.TypeOf<PlatinumCustomer>());
    }
}
