using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace App1.Models
{
    public class MoviesRepository : IMoviesRepository
    {
        private ObservableCollection<Movie> movies = new ObservableCollection<Movie>();

        public MoviesRepository()
        {
            movies.Add(new Movie { Title="Evil Destination" });
            movies.Add(new Movie { Title = "Sos" });
            movies.Add(new Movie { Title = "Snowroller" });
            movies.Add(new Movie { Title = "Jasfjjkasjk" });
        }
        
        public ObservableCollection<Movie> GetAllMovies()
        {
            return movies;
        }
    }
}
