using Bogus;
using Gorev12.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace Gorev12.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<PostEntity> Posts { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var users = new List<UserEntity>
            {
                new UserEntity { Id = 1, Username = "Alice", Password = "password1", Email = "alice.johnson@google.com", CreatedAt = DateTimeOffset.UtcNow },
                new UserEntity { Id = 2, Username = "David", Password = "password2", Email = "david.smith@google.com", CreatedAt = DateTimeOffset.UtcNow },
                new UserEntity { Id = 3, Username = "Emily", Password = "password3", Email = "emily.brown@google.com", CreatedAt = DateTimeOffset.UtcNow },
                new UserEntity { Id = 4, Username = "Michael", Password = "password4", Email = "michael.davis@google.com", CreatedAt = DateTimeOffset.UtcNow },
                new UserEntity { Id = 5, Username = "Olivia", Password = "password5", Email = "olivia.martinez@google.com", CreatedAt = DateTimeOffset.UtcNow }
            };

            modelBuilder.Entity<UserEntity>().HasData(users);

            var postFaker = new Faker<PostEntity>()
                .RuleFor(p => p.Id, f => f.IndexFaker + 1)
                .RuleFor(p => p.Title, f => f.Lorem.Sentence())
                .RuleFor(p => p.Content, f => f.Lorem.Paragraph())
                .RuleFor(p => p.CoverImageUrl, f => f.Image.PicsumUrl())
                .RuleFor(p => p.CreatedAt, f => f.Date.PastOffset())
                .RuleFor(p=>p.CoverImageInt, f=>f.Random.Number(1,1000))
                .RuleFor(p => p.CreatedBy, f => f.PickRandom(users).Id);

            var posts = postFaker.Generate(100);

            modelBuilder.Entity<PostEntity>().HasData(posts);

            var commentFaker = new Faker<CommentEntity>()
                .RuleFor(c => c.Id, f => f.IndexFaker + 1)
                .RuleFor(c => c.PostId, f => f.PickRandom(posts).Id)
                .RuleFor(c => c.Email, f => f.PickRandom(users).Email)
                .RuleFor(c => c.Content, f => f.Lorem.Sentence())
                .RuleFor(c => c.CreatedAt, f => f.Date.PastOffset())
                .RuleFor(c => c.CreatedBy, f => f.PickRandom(users).Id);

            var comments = new List<CommentEntity>();
            foreach (var post in posts)
            {
                var commentCount = new Random().Next(3, 6); 
                for (int i = 0; i < commentCount; i++)
                {
                    var comment = commentFaker.Generate();
                    comment.PostId = post.Id;
                    comments.Add(comment);
                }
            }

            modelBuilder.Entity<CommentEntity>().HasData(comments);
        }
    }
}
