using App1.Models;
using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

namespace App1.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        Realm realm = Realm.GetInstance();

        public IRealmCollection<Movie> Movies { get; }

        public MainViewModel()
        {
            Movies = realm.All<Movie>().AsRealmCollection();
            AddCommand = new Command(
                execute: () => {
                    realm.Write(() =>
                    {
                        //var maxId = Movies.Max(m => m.MovieID);
                        var movie = new Movie();
                        movie.MovieID = System.Guid.NewGuid().ToString();
                        movie.Title = NewTitle;
                        realm.Add(movie);
                    });
                },
                canExecute: () =>
                {
                    return NewTitle?.Length > 0;
                });
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
                if( selectedMovie != null)
                    selectedMovie.Selected = false;

                SetProperty(ref selectedMovie, value);

                if (selectedMovie != null)
                    selectedMovie.Selected = true;
            }
        }

        public string newTitle;
        public string NewTitle { get
            {
                return newTitle;
            }
            set {
                if( !value.Equals(newTitle))
                {
                    SetProperty(ref newTitle, value);
                    RefreshCanExecute();
                }
            }
        }

        public ICommand AddCommand { private set; get; }

        //Tell all buttons to check there canexecute status again
        private void RefreshCanExecute()
        {
            (AddCommand as Command).ChangeCanExecute();
        }
    }
}
