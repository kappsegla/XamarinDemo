using App1.Models;
using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Realms.Sync;

namespace App1.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        Realm realm;

        public IRealmCollection<Movie> Movies { get; set; }

        private async Task InitializeRealm()
        {
            /*var credentials = Credentials.UsernamePassword("test", "test", createUser: false);
            var serverUrl = new Uri("https://handcrafted-plastic-bacon.de1a.cloud.realm.io");
            var user = await User.LoginAsync(credentials, serverUrl);

            var realmUrl = new Uri("realm://handcrafted-plastic-bacon.de1a.cloud.realm.io/~/userRealm");
            var config = new FullSyncConfiguration(realmUrl,user);

            realm = Realm.GetInstance(config);
            */realm = Realm.GetInstance();
            Movies = realm.All<Movie>().AsRealmCollection();
        }


        public MainViewModel()
        {
            InitializeRealm();
           
            
            AddCommand = new Command(
                execute: () =>
                {
                    realm.WriteAsync((tempRealm) =>
                    {
                        var movie = new Movie();
                        movie.MovieID = System.Guid.NewGuid().ToString();
                        movie.Title = NewTitle;
                        tempRealm.Add(movie);
                    });
                },
                canExecute: () =>
                {
                    return NewTitle?.Length > 0;
                });
            SaveCommand = new Command(
                execute: () =>
                {
                    realm.WriteAsync((tempRealm) =>
                    {
                        tempRealm.Add(selectedMovie, true);
                    });
                },
                canExecute: () => selectedMovie != null
                );

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
                RefreshCanExecute();
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
        public ICommand SaveCommand { private set; get; }

        //Tell all buttons to check there canexecute status again
        private void RefreshCanExecute()
        {
            (AddCommand as Command).ChangeCanExecute();
            (SaveCommand as Command).ChangeCanExecute();
        }
    }
}
