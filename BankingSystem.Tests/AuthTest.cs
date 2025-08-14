using BankingSystem.Api.Models.Request;
using BankingSystem.Core.Common;
using BankingSystem.Core.Entity;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BankingSystem.Tests;

public class AuthTest
{
    private readonly TestBed _fixture;
    public AuthTest()
    {
        _fixture = new TestBed();
    }

    /// <summary>
    /// Create User Test case : Success
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task RegisterUser_ReturnSuccess()
    {
        
        //Arrange
        var request = new UserRegisterRequest()
        {
            Name= "test",
            Email = "test@gmail.com",
            Password = "abcd123",
            ConfirmPassword = "abcd123"
        };

        _fixture._unitofWork.Setup(u => u.Users.AddAsync(It.IsAny<User>()));

        //Act
        var response = await _fixture.AuthController.Register(request);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(response);
        Assert.Equal(200, okResult.StatusCode);
    }

    /// <summary>
    /// Create User Test case : BadRequest 
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task RegisterUser_NameValidation_ReturnFail()
    {

        //Arrange
        var request = new UserRegisterRequest()
        {
            Name = null,
            Email = "test@gmail.com",
            Password = "abcd123",
            ConfirmPassword = "abcd123"
        };
        _fixture.AuthController.ModelState.AddModelError("Name", "Name is required");
        //Act
        await Assert.ThrowsAsync<AppException>(async () =>
        {
            await _fixture.AuthController.Register(request);
        });
    }

    /// <summary>
    /// Model Validation
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task RegisterUser_EmailValidation_ReturnFail()
    {

        //Arrange
        var request = new UserRegisterRequest()
        {
            Name = "asfas",
            Email = null,
            Password = "abcd123",
            ConfirmPassword = "abcd123"
        };
        _fixture.AuthController.ModelState.AddModelError("Email", "Email is required");
        //Act
        await Assert.ThrowsAsync<AppException>(async () =>
        {
            await _fixture.AuthController.Register(request);
        });
    }

    /// <summary>
    /// Model Validation
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task RegisterUser_PasswordValidation_ReturnFail()
    {

        //Arrange
        var request = new UserRegisterRequest()
        {
            Name = "abcd",
            Email = "test@gmail.com",
            Password = "",
            ConfirmPassword = "abcd123"
        };
        _fixture.AuthController.ModelState.AddModelError("Password", "Password is required");
        //Act
        await Assert.ThrowsAsync<AppException>(async () =>
        {
            await _fixture.AuthController.Register(request);
        });
    }
}
