using System.Collections.Generic;
using System.Linq;
using VidlyAPI.Data;
using VidlyAPI.Models;
using VidlyAPI.Repository.IRepository;

namespace VidlyAPI.Repository
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public MoviesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool CreateMovie(Movie movie)
        {
            _dbContext.Movies.Add(movie);
            return Save();
        }

        public bool DeleteMovie(Movie movie)
        {
            _dbContext.Movies.Remove(movie);
            return Save();
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return _dbContext.Movies.ToList().OrderBy(x => x.Name);
        }

        public Movie GetMovie(int id)
        {
            return _dbContext.Movies.SingleOrDefault(x => x.Id == id);
        }

        public bool MovieExcistById(int id)
        {
            return _dbContext.Movies.Any(x => x.Id == id);
        }

        public bool MovieExcistByName(string name)
        {
            return _dbContext.Movies.Any(x => x.Name == name);
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateMovie(Movie movie)
        {
            _dbContext.Movies.Update(movie);
            return Save();
        }
    }
}
