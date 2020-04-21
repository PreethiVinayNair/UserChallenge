using System.Threading.Tasks;
using UserChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace UserChallenge.Controllers
{

  [Route("api/userchallenge")]
  [ApiController]
  public class UserController : Controller
  {
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
      this.userService = userService;
    }

    [HttpGet]
    [Route("")]
    public IActionResult GetUsersList()
    {
      var users = userService.GetUsersList();

      return Ok(users);
    }


    [HttpGet]
    [Route("{id}")]
    public IActionResult Get(String name)
    {
      var user = userService.GetUserByName(name);

      if (user == null)
      {
        return NotFound();
      }

      return Ok(user);
    }

    [HttpPost]
    [Route("")]
    public IActionResult CreateUser([FromBody] UserModel model)
    {
      var user = userService.CreateUser(model);

      return Ok(new { user.Id });
    }
    [HttpPut]
    [Route("{id}")]
    public IActionResult Put([FromBody] UserModel model)
    {
      var user = userService.UpdateUser(model);

      if (user == null)
      {
        return NotFound();
      }

      return Ok(new { user.Id });
    }


    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteUser(Guid id)
    {
      userService.DeleteUser(id);

      return Ok();
    }

  }
}
