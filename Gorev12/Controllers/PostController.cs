using Gorev12.Data;
using Gorev12.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Gorev12.Controllers
{
    public class PostController : Controller
    {
        private readonly AppDbContext _dbcontext;

        public PostController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchTerm, int endPage, int startPage, string searchUser, string searchContent, string searchEmail, int page = 1, int pageSize = 10)
        {
            ViewData["NameSortParm"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["IdSortParm"] = sortOrder == "postId" ? "postId_desc" : "postId";
            ViewData["ContentSortParm"] = sortOrder == "content" ? "content_desc" : "content";
            ViewData["DateSortParm"] = sortOrder == "date" ? "date_desc" : "date";
            ViewData["UserSortParm"] = sortOrder == "users" ? "users_desc" : "users";
            ViewData["EmailSortParm"] = sortOrder == "email" ? "email_desc" : "email";
            ViewData["CommentsSortParm"] = sortOrder == "comments" ? "comments_desc" : "comments";

            //searchBar - order
            var filteredPosts = from s in _dbcontext.Posts.Include(p => p.Comments)
                                select s;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                filteredPosts = filteredPosts.Where(p => p.Title.Contains(searchTerm));
            }
            if (!string.IsNullOrEmpty(searchUser))
            {
                filteredPosts = filteredPosts.Where(p => p.User.Username.Contains(searchUser));
            }
            if (!string.IsNullOrEmpty(searchContent))
            {
               filteredPosts = filteredPosts.Where(p => p.Content.Contains(searchContent));
            }
            if (!string.IsNullOrEmpty(searchEmail))
            {
                filteredPosts = filteredPosts.Where(p => p.User.Email.Contains(searchEmail));
            }

            //switch-case
            switch (sortOrder)
            {
                case "name_desc":
                    filteredPosts = filteredPosts.OrderByDescending(s => s.Title);
                    break;
                case "date":
                    filteredPosts = filteredPosts.OrderBy(s => s.CreatedAt);
                    break;
                case "date_desc":
                    filteredPosts = filteredPosts.OrderByDescending(s => s.CreatedAt);
                    break;
                case "postId":
                    filteredPosts = filteredPosts.OrderBy(s => s.Id);
                    break;
                case "postId_desc":
                    filteredPosts = filteredPosts.OrderByDescending(s => s.Id);
                    break;
                case "content":
                    filteredPosts = filteredPosts.OrderBy(s => s.Content);
                    break;
                case "content_desc":
                    filteredPosts = filteredPosts.OrderByDescending(s => s.Content);
                    break;
                case "users":
                    filteredPosts = filteredPosts.OrderBy(s => s.User.Username);
                    break;
                case "users_desc":
                    filteredPosts = filteredPosts.OrderByDescending(s => s.User.Username);
                    break;
                case "email":
                    filteredPosts = filteredPosts.OrderBy(s => s.User.Email);
                    break;
                case "email_desc":
                    filteredPosts = filteredPosts.OrderByDescending(s => s.User.Email);
                    break;
                case "comments":
                    filteredPosts = filteredPosts.OrderBy(p => p.Comments.Count());
                    break;
                case "comments_desc":
                    filteredPosts = filteredPosts.OrderByDescending(p => p.Comments.Count());
                    break;
                default:
                    filteredPosts = filteredPosts.OrderBy(s => s.Title);
                    break;
            }

            //pagination
            var skipAmount = (page - 1) * pageSize;
            var pagedPosts = await filteredPosts.Skip(skipAmount).Take(pageSize).ToListAsync();

            var viewModel = new PostIndexViewModel
            {
                Posts = pagedPosts,
                Users = await _dbcontext.Users.ToListAsync(),
                Comments = await _dbcontext.Comments.ToListAsync(),
                Pagination = new Pagination
                {
                    PageNumber = page,
                    PageSize = pageSize,
                    TotalItems = await filteredPosts.CountAsync(),
                    StartPage = startPage,
                    EndPage = endPage,
                }
            };
            return View(viewModel);
        }
    }
}
