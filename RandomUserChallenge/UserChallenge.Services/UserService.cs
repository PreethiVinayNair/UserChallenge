using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserChallenge.Domain;
using UserChallenge.Infrastructure.Extensions;
using Microsoft.Extensions.Options;
using System.Linq;
using UserChallenge.Domain.Entities;

namespace UserChallenge.Services
{
  class UserService :IUserService
  {
    private readonly ApplicationDbContext context;
    private readonly AppSettings appSettings;

    public UserService(ApplicationDbContext context, IOptions<AppSettings> appSettings)
    {
      this.context = context;
      this.appSettings = appSettings.Value;
    }
  }
}
