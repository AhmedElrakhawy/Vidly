using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VidlyAPI.Models;
using VidlyAPI.Models.DTO;
using VidlyAPI.Repository.IRepository;

namespace VidlyAPI.Controllers
{
    [Route("api/v{version:apiVersion}/Genre")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _GenreRepository;
        private readonly IMapper _Mapper;
        public GenreController(IGenreRepository genreRepository, IMapper mapper)
        {
            _GenreRepository = genreRepository;
            _Mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<GenreDto>))]
        public ActionResult GetAllGenre()
        {
            var Genres = _GenreRepository.GetAllGenre();
            var GenresDto = new List<GenreDto>();
            foreach (var genre in Genres)
                GenresDto.Add(_Mapper.Map<GenreDto>(genre));
            return Ok(GenresDto);
        }
        [HttpGet("{Id:int}", Name = "GetGenre")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(GenreDto))]
        public ActionResult GetGenre(int Id)
        {
            var Genre = _GenreRepository.GetGenre(Id);
            if (Genre == null)
                return NotFound();
            var GenreDto = _Mapper.Map<GenreDto>(Genre);
            return Ok(GenreDto);
        }
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Genre))]
        [ProducesResponseType(500)]
        public ActionResult CreateGenre([FromBody] GenreDto genreDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var Genre = _Mapper.Map<Genre>(genreDto);
            if (!_GenreRepository.CreateGenre(Genre))
            {
                ModelState.AddModelError("", $"SomeThing Went Wrong When Creating {Genre.Name}");
                return StatusCode(500, ModelState);
            }
            else
            {
                return CreatedAtRoute("GetGenre", new { Id = Genre.Id }, Genre);
            }
        }
    }
}
