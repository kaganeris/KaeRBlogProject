using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Models.DTOs.GenreDTOs;
using Project.Application.Models.DTOs.PostDTOs;
using Project.Application.Services.Abstract;
using System.Security.Claims;

namespace Project.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PostController : Controller
    {
        private readonly IGenreService genreService;
        private readonly IAuthorService authorService;
        private readonly IPostService postService;

        public PostController(IGenreService genreService,IAuthorService authorService,IPostService postService)
        {
            this.genreService = genreService;
            this.authorService = authorService;
            this.postService = postService;
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
                {
                    TempData["UpdateSuccess"] = $"{createPostDTO.Title} Başlıklı Post Başarıyla Oluşturuldu.";
                    return RedirectToAction("Index", "Home");
                }
                else
                    return View(createPostDTO);
            }
            return View(createPostDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber)
        {
            List<PostListDTO> postLists = await postService.GetPostList();
            ViewBag.TotalPostCount = postLists.Count;

            return View(postLists.Skip((pageNumber * 10) - 10).Take(10).ToList());
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            UpdatePostDTO updatePostDTO = await postService.GetPostById(id);
            updatePostDTO.Genres = await genreService.GetGenreList();
            return View(updatePostDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePostDTO updatePostDTO)
        {
            if (ModelState.IsValid)
            {
                bool result = await postService.UpdatePost(updatePostDTO);
                if (result)
                {
                    TempData["UpdateSuccess"] = $"{updatePostDTO.Id} ID'li Post Başarıyla Güncellendi";
                    return RedirectToAction("Index", "Post");
                }
                else
                {
                    updatePostDTO.Genres = await genreService.GetGenreList();
                    return View(updatePostDTO);
                }
            }
            else
            {
                updatePostDTO.Genres = await genreService.GetGenreList();
                return View(updatePostDTO);
            }

        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await postService.DeletePost(id);
            if (result)
            {
                TempData["Delete"] = $"{id} ID'li Post Başarıyla Pasifleştirildi";
                return RedirectToAction("Index", "Post");
            }
            else
            {
                TempData["Delete"] = $"{id} ID'li Post Pasifleştirilemedi";
                return RedirectToAction("Index", "Post");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Active(int id)
        {
            bool result = await postService.ActivePost(id);
            if (result)
            {
                TempData["Active"] = $"{id} ID'li Post Başarıyla Aktifleştirildi";
                return RedirectToAction("Index", "Post");
            }
            else
            {
                TempData["Active"] = $"{id} ID'li Post Aktifleştirilemedi";
                return RedirectToAction("Index", "Post");
            }
        }
    }
}
