using FinancialSystem;
using FinancialSystem.Interfaces;
using FinancialSystem.Models.UserModels;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FSTests
{
    public class UserControllerTest
    {
        private readonly UserController _controller;
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public UserControllerTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _controller = new UserController(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task GetUserAsync_ReturnOk_WhenUserExist()
        {
            //Arrange(Preparar)
            var userList = new List<UserList>
            {
                new UserList {UserId = 1, UserName = "Eloy"},
                new UserList {UserId = 2, UserName = "Kamila"}
            };
            _userRepositoryMock.Setup(r => r.GetUsersAsync()).ReturnsAsync(userList);

            //Act(Actuar)
            var result = await _controller.GetUsersAsync();

            //Assert(Afirmar)
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUsers = Assert.IsType<List<UserList>>(okResult.Value);
            Assert.NotEmpty(returnedUsers);
            Assert.Equal(2, returnedUsers.Count);
        }

        [Fact]
        public async Task GetUserAsync_ReturnOk_WhenNoUserExist()
        {
            //Arrange(Preparar)
            _userRepositoryMock.Setup(r => r.GetUsersAsync()).ReturnsAsync(new List<UserList>());

            //Act(Actuar)
            var result = await _controller.GetUsersAsync();

            //Assert(Afirmar)
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("No existen usuarios", notFoundResult.Value);
        }
    }
}