using System.Collections.Generic;
using Gorev12.Data;
using Gorev12.Data.Entity;

namespace Gorev12.Models
{
    public class PostIndexViewModel
    {
        public List<PostEntity> Posts { get; set; }
        public List<UserEntity> Users { get; set; }
        public List<CommentEntity> Comments { get; set; }
        public Pagination Pagination { get; set; }
    }
}
