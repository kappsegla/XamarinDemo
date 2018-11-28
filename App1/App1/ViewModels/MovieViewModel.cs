using App1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App1.ViewModels
{
    public class MovieViewModel : BaseViewModel
    {
        public Movie movie;

        public MovieViewModel(Movie movie)
        {
            this.movie = movie;
        }

        public string MovieID
        {
            get
            {
                return movie.MovieID;
            }
            set
            {
                movie.MovieID = value;
            }
        }
        public string Title
        {
            get
            {
                return movie.Title;
            }
            set
            {
                movie.Title = value;
            }
        }

        public bool Selected { get; set; }
    }
}
