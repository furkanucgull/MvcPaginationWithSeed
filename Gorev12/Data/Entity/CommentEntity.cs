using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Bogus.DataSets;

namespace Gorev12.Data.Entity
{
    public class CommentEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }
        public PostEntity Post { get; set; }

        public string Content { get; set; }
        public string Email { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(User))]
        public int? CreatedBy { get; set; }
        public UserEntity User { get; set; }
    }
}
