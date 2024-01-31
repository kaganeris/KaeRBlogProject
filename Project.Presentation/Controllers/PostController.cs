using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Models.DTOs.PostDTOs;
using Project.Application.Models.VMs.PostVMs;
using Project.Application.Services.Abstract;
using System.Security.Claims;

namespace Project.Presentation.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly IGenreService genreService;
        private readonly IAuthorService authorService;

        public PostController(IPostService postService, IGenreService genreService, IAuthorService authorService)
        {
            this.postService = postService;
            this.genreService = genreService;
            this.authorService = authorService;
        }

        [HttpGet]
        [Authorize(Roles = "Author,Admin")]
        public async Task<IActionResult> Create()
        {
            CreatePostDTO createPostDTO = new CreatePostDTO();
            createPostDTO.Genres = await genreService.GetGenreList();
            return View(createPostDTO);
        }

        [HttpPost]
        [Authorize(Roles = "Author,Admin")]
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

        [HttpGet]
        [Authorize(Roles = "Author,Admin")]
        public async Task<IActionResult> Update(int id)
        {
            UpdatePostDTO updatePostDTO = await postService.GetPostById(id);
            updatePostDTO.Genres = await genreService.GetGenreList();
            return PartialView("_PostUpdatePopup", updatePostDTO);

        }
        [HttpPost]
        [Authorize(Roles = "Author,Admin")]
        public async Task<IActionResult> Update(UpdatePostDTO updatePostDTO)
        {
            if (ModelState.IsValid)
            {
                bool result = await postService.UpdatePost(updatePostDTO);
                if (result)
                {
                    return Json("Ok");
                }
                else
                {
                    return Json("Hata");
                }
            }
            else
            {
                return Json("Hata");
            }

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await postService.DeletePost(id);
            if (result)
            {
                return Json("Ok");
            }
            else
                return Json("Hata");
        }


        [HttpGet]
        public async Task<IActionResult> GetHeroPosts()
        {
            List<PostHeroDTO> postHeros = await postService.GetHeroPosts();
            return PartialView("_HeroPartial", postHeros);
        }

        [HttpGet]
        public async Task<IActionResult> GetGridBigPost(string genreName)
        {
            var userIDClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIDClaim == null)
            {
                PostGridVM postGridVM = await postService.GetPostGridVM(genreName, Guid.NewGuid());
                return PartialView("_TodayMostClickPostOnePartial", postGridVM);
            }
            else
            {
                string userID = userIDClaim.Value;
                PostGridVM postGridVM = await postService.GetPostGridVM(genreName, Guid.Parse(userID));
                return PartialView("_TodayMostClickPostOnePartial", postGridVM);

            }

        }

        [HttpGet]
        public async Task<IActionResult> GetGridPost(string genreName)
        {
            var userIDClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIDClaim == null)
            {
                PostGridVM postGridVM = await postService.GetPostGridVM(genreName, Guid.NewGuid());
                return PartialView("_TodayMostClickGridPostPartial", postGridVM);
            }
            else
            {
                string userID = userIDClaim.Value;
                PostGridVM postGridVM = await postService.GetPostGridVM(genreName, Guid.Parse(userID));
                if (postGridVM == null)
                {
                    return Json("Hata");
                }
                else
                {
                    return PartialView("_TodayMostClickGridPostPartial", postGridVM);

                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> SinglePost(int postID)
        {
            var userIDClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIDClaim == null)
            {
                PostDetailVM postDetailVM = await postService.GetDetailPost(postID, Guid.NewGuid());
                await postService.IncreaseClickCount(postID);
                return View(postDetailVM);
            }
            else
            {
                string userID = userIDClaim.Value;

                PostDetailVM postDetailVM = await postService.GetDetailPost(postID, Guid.Parse(userID));
                if (postDetailVM == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    await postService.IncreaseClickCount(postID);
                    return View(postDetailVM);
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> FirstSectionPosts(string genreName)
        {
            var userIDClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIDClaim == null)
            {
                List<PostGridVM> postGridVM = await postService.GetSectionPosts(genreName, Guid.NewGuid());
                return PartialView("_FirstSectionPartial", postGridVM);
            }
            else
            {
                string userID = userIDClaim.Value;
                List<PostGridVM> postGridVM = await postService.GetSectionPosts(genreName, Guid.Parse(userID));
                return PartialView("_FirstSectionPartial", postGridVM);

            }

        }

        [HttpGet]
        public async Task<IActionResult> SecondSectionPosts(string genreName)
        {
            var userIDClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIDClaim == null)
            {
                List<PostGridVM> postGridVM = await postService.GetSectionPosts(genreName, Guid.NewGuid());
                return PartialView("_SecondSectionPartial", postGridVM);
            }
            else
            {
                string userID = userIDClaim.Value;
                List<PostGridVM> postGridVM = await postService.GetSectionPosts(genreName, Guid.Parse(userID));
                return PartialView("_SecondSectionPartial", postGridVM);

            }

        }

        [HttpGet]
        public async Task<IActionResult> ThirdSectionPosts(string genreName)
        {
            var userIDClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIDClaim == null)
            {
                List<PostGridVM> postGridVM = await postService.GetSectionPosts(genreName, Guid.NewGuid());
                return PartialView("_ThirdSectionPartial", postGridVM);
            }
            else
            {
                string userID = userIDClaim.Value;
                List<PostGridVM> postGridVM = await postService.GetSectionPosts(genreName, Guid.Parse(userID));
                return PartialView("_ThirdSectionPartial", postGridVM);

            }

        }

        [HttpGet]
        public async Task<IActionResult> GetTrendPosts()
        {
            List<PostGridVM> postGridVM = await postService.GetTrendingPosts();

            return PartialView("_TrendPostsPartial", postGridVM);

        }

        [HttpGet]
        public async Task<IActionResult> GetPopulerPosts()
        {
            List<PostGridVM> postGridVM = await postService.GetPopulerPosts();

            return PartialView("_PopulerPostsPartial", postGridVM);

        }
        [HttpGet]
        public async Task<IActionResult> GetLatestPosts()
        {
            List<PostGridVM> postGridVM = await postService.GetLatestPosts();

            return PartialView("_LatestPostsPartial", postGridVM);

        }
        [HttpGet]
        public async Task<IActionResult> GetLatestFooterPosts()
        {
            List<PostGridVM> postGridVM = await postService.GetLatestPosts();
            List<PostGridVM> deneme = postGridVM.Take(4).ToList();
            return PartialView("_LatestPostsFooterPartial", deneme);

        }

        [HttpGet]
        public async Task<IActionResult> GetDetailTrendPosts()
        {
            List<PostGridVM> postGridVM = await postService.GetTrendingPosts();

            return PartialView("_TrendPostsDetailPartial", postGridVM);

        }

        [HttpGet]
        public async Task<IActionResult> ProfilePosts()
        {
            var userIDClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            string userID = userIDClaim.Value;

            List<PostDetailVM> postDetailVMs = await postService.GetProfilePosts(Guid.Parse(userID));

            return PartialView("_ProfilePostPartial", postDetailVMs);

        }

        [HttpPost]
        public async Task<IActionResult> GetCategoryPosts(string categoryName,int pageNumber)
        {
            var userIDClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIDClaim == null)
            {
                List<PostGridVM> postGridVM = await postService.GetCategoryPostsByPageNumber(categoryName, pageNumber, Guid.NewGuid());
                return PartialView("_CategoryPostsByPageNumberPartial", postGridVM);
            }
            else
            {
                string userID = userIDClaim.Value;
                List<PostGridVM> postGridVM = await postService.GetCategoryPostsByPageNumber(categoryName, pageNumber, Guid.Parse(userID));
                return PartialView("_CategoryPostsByPageNumberPartial", postGridVM);

            }
        }
    }
}
