using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace App1.Models
{
    public interface IMoviesRepository
    {
        ObservableCollection<Movie> GetAllMovies();
    }
}