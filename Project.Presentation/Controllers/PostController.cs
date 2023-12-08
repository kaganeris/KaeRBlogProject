using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Models.DTOs.PostDTOs;
using Project.Application.Services.Abstract;
using System.Security.Claims;

namespace Project.Presentation.Controllers
{
    [Authorize(Roles = "Author,Admin")]
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly IGenreService genreService;
        private readonly IAuthorService authorService;

        public PostController(IPostService postService,IGenreService genreService,IAuthorService authorService)
        {
            this.postService = postService;
            this.genreService = genreService;
            this.authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreatePostDTO createPostDTO = new CreatePostDTO();
            createPostDTO.Genres = await genreService.GetGenreList();
            return View(createPostDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostDTO createPostDTO)
        {
            if (ModelState.IsValid)
            {
                var userIDClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

                string userID = userIDClaim.Value;

                createPostDTO.AuthorId = await authorService.GetAuthorIdByUserId(Guid.Parse(userID));
                bool result = await postService.CreatePost(createPostDTO);
                if (result == true)
                    return RedirectToAction("Index", "Home");
                else
                    return View(createPostDTO);
            }
            return View(createPostDTO);
        }
    }
}
