using System.Collections.Generic;
using VidlyAPI.Models;

namespace VidlyAPI.Repository.IRepository
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetAllGenre();
        Genre GetGenre(int id);
        bool CreateGenre(Genre genre);
        bool Save();
    }
}
