using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Primitives;
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
        Title = model.Title,
        FirstName = model.FirstName,
        LastName = model.LastName,
        Email = model.Email,
        DOB = model.DOB.Date,
        Phone = model.Phone,
        Image = (model.Image.Length) > 0 ? Encoding.ASCII.GetBytes(model.Image):null
      };

      context.users.Add(newUser);
      context.SaveChanges();

      return newUser;
    }

    public UserModel[] GetUsersList(int limit)
    {

      //Should add Take limit
      return context.users.Select(user => new UserModel
      {
        Id = user.Id,
        Title = user.Title,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email,
        DOB=user.DOB.Date,
        Image = (user.Image.Length) >0 ?  Encoding.ASCII.GetString(user.Image):null,
        Phone = user.Phone
      }).Take(limit).ToArray();
    }

    public UserModel GetUserById(Guid Id)
    {
      return context.users.Where(user => user.Id == Id).Select(user => new UserModel
      {
        Id = user.Id,
        Title = user.Title,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email,
        DOB = user.DOB,
        Image = (user.Image.Length) > 0 ? Encoding.ASCII.GetString(user.Image):null,
        Phone = user.Phone
      }).SingleOrDefault();
    }

    public UserModel SearchUserByFirstName(string FirstName)
    {
      return context.users.Where(user=>user.FirstName.Contains(FirstName)).Select(user => new UserModel
      {
        Id = user.Id,
        Title = user.Title,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email,
        DOB = user.DOB.Date,
        Image = (user.Image.Length) > 0 ? Encoding.ASCII.GetString(user.Image) : null,
        Phone = user.Phone
      }).SingleOrDefault();
    }

    public UserModel SearchUserByLastName(string LastName)
    {
      return context.users.Where(user => user.LastName.Contains(LastName)).Select(user => new UserModel
      {
        Id = user.Id,
        Title = user.Title,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email,
        DOB = user.DOB.Date,
        Image = (user.Image.Length) > 0 ? Encoding.ASCII.GetString(user.Image) : null,
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

      updatedUser.Title = model.Title;
      updatedUser.FirstName = model.FirstName;
      updatedUser.LastName = model.LastName;
      updatedUser.Email = model.Email;
      updatedUser.DOB = model.DOB.Date;
      updatedUser.Image = (model.Image.Length) > 0 ? Encoding.ASCII.GetBytes(model.Image) : null;
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
