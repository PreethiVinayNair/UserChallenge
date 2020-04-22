using System.Threading.Tasks;
using UserChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;

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
    public IActionResult GetUsersList(int limit,string FirstName=null, string LastName=null)
    {
      try
      {
        if (!(String.IsNullOrEmpty(FirstName)))
        {
          var user = userService.SearchUserByFirstName(FirstName);

          return Ok(user);
        }

        if (!(String.IsNullOrEmpty(LastName)))
        {
          var user = userService.SearchUserByLastName(LastName);

          return Ok(user);
        }
        var users = userService.GetUsersList(limit);
        return Ok(users);
      }
       catch (Exception)
      {
        return RedirectToAction("Error", "Home");
      }
    }


    [HttpGet]
    [Route("{id}")]
    public IActionResult GetById(Guid id)
    {
      try
      {
        var user = userService.GetUserById(id);

        if (user == null)
        {
          return NotFound();
        }

        return Ok(user);
      }
      catch(Exception)
      { return RedirectToAction("Error", "Home"); }
    }


    [HttpPost]
    [Route("")]
    public IActionResult CreateUser([FromBody] UserModel model)
    {
      try
      {
        var user = userService.CreateUser(model);
        return Ok(new { user.Id });
      }
      catch (Exception)
      {
        return RedirectToAction("Error", "Home"); }
    }

    [HttpPut]
    [Route("{id}")]
    public  IActionResult Put([FromBody] UserModel model)
    {
      try
      {
        var user = userService.UpdateUser(model);

        if (user == null)
        {
          return NotFound();
        }

        return Ok(new { user.Id });
      }
      catch (Exception)
      {
        return RedirectToAction("Error", "Home");
      }
    }


    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteUser(Guid id)
    {
      try { 
      userService.DeleteUser(id);
      return Ok();
    }
       catch (Exception)
      {
        return RedirectToAction("Error", "Home");
      }
    }

  }
}
