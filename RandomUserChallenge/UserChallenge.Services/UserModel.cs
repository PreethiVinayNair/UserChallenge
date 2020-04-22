using System;
using System.Collections.Generic;
using System.Text;

namespace UserChallenge.Services
{
 public class UserModel
  {
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    //Should include Title, First and Last Name
    public string Email { get; set; }

    public string Phone{ get; set; }

        public DateTime DOB { get; set; }

    //Profile Images
    public string Image { get; set; }
  }
}
