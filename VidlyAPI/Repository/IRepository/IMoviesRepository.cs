using System.Collections.Generic;
using VidlyAPI.Models;

namespace VidlyAPI.Repository.IRepository
{
    public interface IMoviesRepository
    {
        bool Save();
        bool CreateMovie(Movie movie);
        bool DeleteMovie(Movie movie);
        bool UpdateMovie(Movie movie);
        Movie GetMovie(int id);
        IEnumerable<Movie> GetAllMovies();
        bool MovieExcistByName(string name);
        bool MovieExcistById(int id);
    }
}
