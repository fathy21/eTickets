using eTickets.Data.Static;
using eTickets.Repositores;
using eTickets.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eTickets.Controllers
{
    public class MovieController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public MovieController(IUnitOfWork unitOfWork )
        {
            this.unitOfWork = unitOfWork;
        }
      //  [Authorize (Roles =UserRoles.User)] 
        public async Task<IActionResult> Index()
        {
            var movies = await unitOfWork.Movies.GetAllWithCinema();
            return View(movies);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await unitOfWork.Movies.GetAllWithCinema();

            if (!string.IsNullOrEmpty(searchString))
            {
                
                var filteredResultNew = allMovies.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Discription, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResultNew);
            }

            return View("Index", allMovies);
        }

        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await unitOfWork.Movies.GetMovieByIdAsync(id);
            return View(movieDetail);
        }

        public async Task<IActionResult> Create()
        {
            var movieDropdownsData = await unitOfWork.Movies.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await unitOfWork.Movies.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(movie);
            }

            await unitOfWork.Movies.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

            public async Task<IActionResult> Edit(int id)
            {
                var movieDetails = await unitOfWork.Movies.GetMovieByIdAsync(id);
                if (movieDetails == null) return View("NotFound");

                var response = new NewMovieVM()
                {
                    Id = movieDetails.Id,
                    Name = movieDetails.Name,
                    Description = movieDetails.Discription,
                    Price = movieDetails.Price,
                    StartDate = movieDetails.StartDate,
                    EndDate = movieDetails.EndDate,
                    ImageURL = movieDetails.ImageUrl,
                    MovieCategory = movieDetails.MovieCategory,
                    CinemaId = movieDetails.CinemaId,
                    ProducerId = movieDetails.ProducerId,
                    ActorIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList(),
                };

                var movieDropdownsData = await unitOfWork.Movies.GetNewMovieDropdownsValues();
                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(response);
            }

            [HttpPost]
            public async Task<IActionResult> Edit(int id, NewMovieVM movie)
            {
                if (id != movie.Id) return View("NotFound");

                if (!ModelState.IsValid)
                {
                    var movieDropdownsData = await unitOfWork.Movies.GetNewMovieDropdownsValues();

                    ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                    ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                    ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                    return View(movie);
                }

                 await unitOfWork.Movies.UpdateMovieAsync(movie);
                return RedirectToAction(nameof(Index));

            }
        
    }
}
