using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using UserChallenge.Domain.Entities;

namespace UserChallenge.Services
{
  public interface IUserService
  {
    User CreateUser(UserModel model);
    User UpdateUser(UserModel model);
    void DeleteUser(Guid id);
    UserModel[] GetUsersList();
    UserModel GetUserById(Guid Id);
  }
}
