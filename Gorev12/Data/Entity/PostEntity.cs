using Bogus.DataSets;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gorev12.Data.Entity
{
    public class PostEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? CoverImageUrl{ get; set; }
        public int? CoverImageInt { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
       [ForeignKey(nameof(User))]
        public int? CreatedBy { get; set; }
        public UserEntity User { get; set; }
        public ICollection<CommentEntity> Comments { get; set; }
    }
}
