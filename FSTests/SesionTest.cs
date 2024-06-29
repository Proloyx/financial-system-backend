using AutoMapper;
using FinancialSystem;
using FinancialSystem.Models;
using FinancialSystem.Models.DB.DBModels;
using FinancialSystem.Models.UserModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;

namespace FSTests;

public class SesionControllerTests
{
    private readonly SesionController _sesionController;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<AppDbContext> _mockContext;
    private readonly List<User> users = new List<User>{ new User { UserId = 15, UserName = "pepe", Email = "pepe@example.com", Password = "123" } };
    
    
    public SesionControllerTests(){
        _mockMapper = new Mock<IMapper>();
        _mockContext = new Mock<AppDbContext>();
        _sesionController = new SesionController(_mockMapper.Object, _mockContext.Object);
    }

    [Fact]
    public async void LoginAsync_BadRequestObjectResult_WithCredntialsInvalid(){
        // Arrange
        var login = new UserLogin{ Email = "pepe@example.com", Password = "1234" };
        _mockContext.Setup(m => m.Users).ReturnsDbSet(users);
        
        // Act
        var badRequest = await _sesionController.LoginAsync(login);
        
        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(badRequest.Result);
        Assert.Equal("Credenciales Inválidas", badRequestResult.Value);
    }

    // [Fact]
    // public async void LoginAsync_OkObjectResult_WithBearer(){
    //     // Arrange
    //     var login = new UserLogin{ Email = "pepe@example.com", Password = "123" };
    //     var user = users.ElementAt(0);
    //     var userRet = new { UserId = user.UserId, UserName = user.UserName, Email = user.Email };
    //     Console.WriteLine(userRet.Email);
    //     _mockContext.Setup(m => m.Users).ReturnsDbSet(users);
        
    //     // Act
    //     var result = await _sesionController.LoginAsync(login);

    //     // Assert
    //     var okResult = Assert.IsType<OkObjectResult>(result.Result);
    // }

    [Fact]
    public async Task RegisterAsync_BadRequestObjectResult_WhenEmailExists()
    {
        // Arrange
        var userRegister = new UserRegister { UserName = "pepe", Email = "pepe@example.com", Password = "123" };
        _mockContext.Setup(m => m.Users).ReturnsDbSet(users);
        
        // Act
        var result = await _sesionController.RegisterAsync(userRegister);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Existe un usuario con ese correo", badRequestResult.Value);
    }

    [Fact]
    public async Task RegisterAsync_OkObjectResult_UserRegistered()
    {
        // Arrange
        var userRegister = new UserRegister { UserName = "pepe", Email = "pepea@example.com", Password = "123" };
        _mockContext.Setup(m => m.Users).ReturnsDbSet(users);
        _mockContext.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);

        // Act
        var result = await _sesionController.RegisterAsync(userRegister);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Se registró correctamente el usuario", okObjectResult.Value);
        Assert.Single(users);
        Assert.Equal("pepe", users.First().UserName);
    }
}