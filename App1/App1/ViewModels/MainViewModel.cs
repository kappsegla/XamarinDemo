using App1.Models;
using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace App1.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        Realm realm = Realm.GetInstance();

        public IRealmCollection<Movie> Movies { get; }

        public MainViewModel()
        {
            Movies = realm.All<Movie>().AsRealmCollection();
        }

        Movie selectedMovie;
        public Movie SelectedMovie
        {
            get
            {
                return selectedMovie;
            }

            set
            {
                SetProperty(ref selectedMovie, value);
            }
        }
    }
}
