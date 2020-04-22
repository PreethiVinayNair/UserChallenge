using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserChallenge.Domain.Entities
{
 public class User
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
     //Should include Title, First and Last Name
    public string Email { get; set; }

    public string Phone { get; set; }

    [Column(TypeName = "date")]
    public DateTime DOB { get; set; }
    //Profile Images
    public byte[] Image { get; set; }
  }
}
