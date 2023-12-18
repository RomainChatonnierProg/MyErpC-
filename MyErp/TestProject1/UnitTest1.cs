using MyErp.Metier;
using MyErp.Entities;
using TestProject1.Mock;

namespace TestProject1;
[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void AddUser_ThrowsException_WhenFirstNameAndLastNameAreEmpty()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var userService = new UserService(userRepositoryMock.Object);
        var invalidUser = new User { FirstName = "", LastName = "" };

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => userService.AddUser(invalidUser));
        Assert.Equal("A user must have a society name OR a first name and last name.", exception.Message);
    }
    
    [TestMethod]
    public void AddUser_DoesNotThrowException_WhenOnlyFirstNameIsProvided()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var userService = new UserService(userRepositoryMock.Object);
        var userWithOnlyFirstName = new User { FirstName = "John", LastName = "" };

        // Act & Assert
        var exceptionRecord = Record.Exception(() => userService.AddUser(userWithOnlyFirstName));
        
        Assert.Null(exceptionRecord);
    }
    
    [TestMethod]
    public void AddUser_ThrowsException_WhenCompanyNameIsNotUnique()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var userService = new UserService(userRepositoryMock.Object);
        
        userRepositoryMock.Setup(repo => repo.CompanyNameExists("ExistingCompanyName")).Returns(true);

        var newUser = new User { CompanyName = "ExistingCompanyName" };

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => userService.AddUser(newUser));
        Assert.Equal("The compagny name must be unique.", exception.Message);
    }
    
    [TestMethod]
    public void AddUser_ThrowsException_WhenFullNameIsNotUnique()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var userService = new UserService(userRepositoryMock.Object);

        // Simuler une situation où le nom-prénom existe déjà
        userRepositoryMock.Setup(repo => repo.FullNameExists("John", "Doe")).Returns(true);

        var newUser = new User { FirstName = "John", LastName = "Doe" };

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => userService.AddUser(newUser));
        Assert.Equal("The fullname must be unique", exception.Message);
    }
    
    [TestMethod]
    public void AddUser_ThrowsException_WhenPostalCodeIsTooLong()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var userService = new UserService(userRepositoryMock.Object);

        var userWithLongPostalCode = new User { PostalCode = "12345678901" }; // 11 caractères

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => userService.AddUser(userWithLongPostalCode));
        Assert.Equal("The postal code must do less than 10 characters", exception.Message);
    }
}