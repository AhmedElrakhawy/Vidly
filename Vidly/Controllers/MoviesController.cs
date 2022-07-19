using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Models;
using Vidly.Repository.IRepository;
using Vidly.ViewModel;

namespace Vidly.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IMoviesRepository _MoviesRepository;
        private readonly IGenreRepository _GenreRepository;
        public MoviesController(IMoviesRepository moviesRepository, IGenreRepository genreRepository)
        {
            _MoviesRepository = moviesRepository;
            _GenreRepository = genreRepository;
        }
        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
                return View();

            return View("Readonly");
        }
        public async Task<JsonResult> GetAllMovies()
        {
            var Movies = await _MoviesRepository.GetAll(SD.MovieUrl);
            var Genres = await _GenreRepository.GetAll(SD.GenreUrl);
            foreach (var movie in Movies)
            {
                movie.Genre = Genres.Where(X => X.Id == movie.GenreId).FirstOrDefault();
            }
            return Json(new { Data = Movies });
        }
        public async Task<IActionResult> Details(int Id)
        {
            var Movie = await _MoviesRepository.GetAsync(SD.MovieUrl, Id);
            var Genres = await _GenreRepository.GetAll(SD.GenreUrl);
            var ViewModel = new MovieFormViewModel(Movie)
            {
                Genres = Genres,
            };
            return View("MovieForm", ViewModel);
        }
        public async Task<IActionResult> MovieForm()
        {
            var Genres = await _GenreRepository.GetAll(SD.GenreUrl);
            var ViewModel = new MovieFormViewModel
            {
                Genres = Genres,
            };
            return View(ViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Save(Movie Movie)
        {
            if (!ModelState.IsValid)
            {
                var ViewModel = new MovieFormViewModel(Movie)
                {
                    Genres = await _GenreRepository.GetAll(SD.GenreUrl)
                };
                return View("MovieForm", ViewModel);
            }
            if (Movie.Id == 0)
            {
                Movie.DateAdded = DateTime.Now;
                await _MoviesRepository.CreateAsync(SD.MovieUrl, Movie);
            }

            else
            {
                await _MoviesRepository.UpdateAsync(SD.MovieUrl, Movie);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var Status = await _MoviesRepository.DeleteAsync(SD.MovieUrl, id);
            if (Status)
            {
                return Json(new { success = true, message = "Deleted Successful" });
            }
            return Json(new { success = false, message = "Deleted Not Successful" });
        }
    }
}
