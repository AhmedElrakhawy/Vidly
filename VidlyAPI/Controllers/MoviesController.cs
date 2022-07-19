using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VidlyAPI.Models;
using VidlyAPI.Models.DTO;
using VidlyAPI.Repository.IRepository;

namespace VidlyAPI.Controllers
{
    [Route("api/v{version:apiVersion}/Movie")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly IMapper _mapper;
        public MoviesController(IMoviesRepository moviesRepository, IMapper mapper)
        {
            _moviesRepository = moviesRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<MovieDto>))]
        public IActionResult GetAllMovies()
        {
            var Movies = _moviesRepository.GetAllMovies();
            var MoviesDto = new List<MovieDto>();
            foreach (var movie in Movies)
                MoviesDto.Add(_mapper.Map<MovieDto>(movie));
            return Ok(MoviesDto);
        }

        [HttpGet("{Id:int}", Name = "GetMovie")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(MovieDto))]
        public IActionResult GetMovie(int Id)
        {
            var Movie = _moviesRepository.GetMovie(Id);
            if (Movie == null)
                return NotFound();
            var MovieDto = _mapper.Map<MovieDto>(Movie);
            return Ok(MovieDto);
        }
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Movie))]
        [ProducesResponseType(500)]
        [ProducesResponseType(204)]
        [ProducesDefaultResponseType]
        public IActionResult CreateMovie([FromBody] MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Movie = _mapper.Map<Movie>(movieDto);
            if (!_moviesRepository.CreateMovie(Movie))
            {
                ModelState.AddModelError("", $"Something Went Wrong With Creating The New Movie {Movie.Name}");
                return StatusCode(500, ModelState);
            }
            else
            {
                return CreatedAtRoute("GetMovie", new { Id = Movie.Id }, Movie);
            }
        }
        [HttpPatch("{Id:int}")]
        [ProducesResponseType(500)]
        [ProducesResponseType(409)]
        [ProducesResponseType(204)]
        public IActionResult UpdateMovie(int Id, [FromBody] MovieDto movieDto)
        {
            if (movieDto == null || movieDto.Id != Id)
            {
                return BadRequest(ModelState);
            }
            var Movie = _mapper.Map<Movie>(movieDto);
            if (!_moviesRepository.UpdateMovie(Movie))
            {
                ModelState.AddModelError("", $"Soething went Wrong When Updating Move {Movie.Name}");
                return StatusCode(500, ModelState);
            }
            else
                return NoContent();
        }
        [HttpDelete("{Id:int}")]
        [ProducesResponseType(500)]
        [ProducesResponseType(409)]
        [ProducesResponseType(204)]
        public IActionResult DeleteMovie(int Id)
        {
            var Status = _moviesRepository.MovieExcistById(Id);
            if (!Status)
            {
                return NotFound(ModelState);
            }
            var Movie = _moviesRepository.GetMovie(Id);
            if (!_moviesRepository.DeleteMovie(Movie))
            {
                ModelState.AddModelError("", $"SomeThing Went Wrong When Deleting Movie {Movie.Name}");
                return StatusCode(500, ModelState);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
