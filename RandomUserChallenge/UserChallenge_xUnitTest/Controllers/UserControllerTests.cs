using System;
using System.Collections.Generic;
using System.Text;
using UserChallenge_xUnitTest.Mocks.Services;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using UserChallenge.Controllers;
using UserChallenge.Services;

namespace UserChallenge_xUnitTest.Controllers
{
  public class UserControllerTests
  {

    [Fact]
    public void UserController_Id_Valid()
    {
      //Arrange
      var mockUser = new UserModel()
      {
        Id = new Guid("9B4553D2-1684-EA11-9996-28C63FBC6C62")
      };

      var mockUserService = new MockUserChallengeService().MockGetByID(mockUser);

      var controller = new UserController(mockUserService.Object);

      //Act
      var result = controller.GetById(new Guid("9B4553D2-1684-EA11-9996-28C63FBC6C62"));

      //Assert
      Assert.IsAssignableFrom<OkObjectResult>(result);
      mockUserService.VerifyGetByID(Times.Once());
    }

    [Fact]
    public void UserController_Id_Invalid()
    {
      //Arrange
      var mockPlayerService = new MockUserChallengeService().MockGetByIDInvalid();

      var controller = new UserController(mockPlayerService.Object);

      //Act
      var result = controller.GetById(new Guid("9B455324-1684-EA11-9996-28C63FBC6C62"));

      //Assert
      Assert.IsAssignableFrom<RedirectToActionResult>(result);
      mockPlayerService.VerifyGetByID(Times.Once());
    }



    [Fact]
    public void UserController_GetUsers_NoUsers()
    {
      //Arrange
      var mockUserChallengeService = new MockUserChallengeService().MockGetUsersList(new UserModel[] { new UserModel() { }});

      var controller = new UserController(mockUserChallengeService.Object);

      int limit = 5;
      //Act
      var result = controller.GetUsersList(limit);
      //Assert
      Assert.IsAssignableFrom<OkObjectResult>(result);
      mockUserChallengeService.VerifyGetAll(Times.Once());
    }

    [Fact]
    public void UserController_GetUsers_Valid()
    {
      //Arrange
      UserModel[] userResults = new UserModel[]
        {
        new UserModel()
        {
          Id=new Guid("9B4553D2-1684-EA11-9996-28C63FBC6C62")
        } };

      var mockUserChallengeService = new MockUserChallengeService().MockGetUsersList(userResults);

      var controller = new UserController(mockUserChallengeService.Object);
      int limit = 5;
      //Act
      var result = controller.GetUsersList(limit);

      //Assert
      Assert.IsAssignableFrom<OkObjectResult>(result);
    }
  }
}
