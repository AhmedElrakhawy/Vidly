using System.Collections.Generic;
using System.Linq;
using VidlyAPI.Data;
using VidlyAPI.Models;
using VidlyAPI.Repository.IRepository;

namespace VidlyAPI.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public GenreRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CreateGenre(Genre genre)
        {
            _dbContext.Genres.Add(genre);
            return Save();
        }

        public IEnumerable<Genre> GetAllGenre()
        {
            return _dbContext.Genres.ToList();
        }

        public Genre GetGenre(int id)
        {
            return _dbContext.Genres.Where(X => X.Id == id).FirstOrDefault();
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0 ? true : false;
        }
    }
}
