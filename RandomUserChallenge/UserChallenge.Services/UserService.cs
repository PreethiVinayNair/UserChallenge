using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserChallenge.Domain;
using Microsoft.Extensions.Options;
using System.Linq;
using UserChallenge.Domain.Entities;

namespace UserChallenge.Services
{
 public class UserService :IUserService
  {
    private readonly ApplicationDbContext context;
    private readonly AppSettings appSettings;

    public UserService(ApplicationDbContext context, IOptions<AppSettings> appSettings)
    {
      this.context = context;
      this.appSettings = appSettings.Value;
    }
    public User CreateUser(UserModel model)
    {
      var newUser = new User
      {
        Name = model.Name,
        Email = model.Email,
        Phone = model.Phone,
        Image = model.Image
      };

      context.users.Add(newUser);
      context.SaveChanges();

      return newUser;
    }

    public UserModel[] GetUsersList()
    {
      return context.users.Select(user => new UserModel
      {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email,
        Image = user.Image,
        Phone = user.Phone
      }).ToArray();
    }

    public UserModel GetUserByName(String Name)
    {
      return context.users.Where(user => user.Name == Name).Select(user => new UserModel
      {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email,
        Image = user.Image,
        Phone = user.Phone
      }).SingleOrDefault();
    }
    public User UpdateUser(UserModel model)
    {
      var updatedUser= context.users.SingleOrDefault(s => s.Id == model.Id);

      if (updatedUser == null)
      {
        return null;
      }

      //TODO: Input validation. Datatype validation is in place in Front end.

      updatedUser.Name = model.Name;
      updatedUser.Email = model.Email;
      updatedUser.Image = model.Image;
      updatedUser.Phone = model.Phone;

      context.SaveChanges();

      return updatedUser;
    }

    public void DeleteUser(Guid id)
    {
      var user = context.users.Find(id);

     context.users.Remove(user);
      context.SaveChanges();
    }
  }
}
