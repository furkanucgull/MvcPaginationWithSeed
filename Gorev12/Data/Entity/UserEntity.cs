using Bogus.DataSets;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gorev12.Data.Entity
{
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
        [ForeignKey(nameof(User))]
        public int? CreatedBy { get; set; }
        public UserEntity User { get; set; }


    }
}
