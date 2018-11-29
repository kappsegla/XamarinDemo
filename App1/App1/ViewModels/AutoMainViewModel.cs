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
using PropertyChanged;
using Credentials = Realms.Sync.Credentials;

[assembly: PropertyChanged.FilterType("App1.ViewModels.AutoMainViewModel")]
namespace App1.ViewModels
{
    //https://github.com/Fody/PropertyChanged    
    [AddINotifyPropertyChangedInterface]
    public class AutoMainViewModel
    {
        private Realm _realm;

        public IEnumerable<Movie> Movies
        {
            get; set;
        }

        private async Task Initialize()
        {
            _realm = await OpenRealm();
            Movies = _realm.All<Movie>().OrderBy(m => m.Title);
        }

        private async Task<Realm> OpenRealm()
        {
            var user = User.Current;
            if (user != null)
            {
                var config = new FullSyncConfiguration(new Uri(Constants.RealmPath, UriKind.Relative), user);
                // User has already logged in, so we can just load the existing data in the Realm.
                return Realm.GetInstance(config);
            }
            var credentials = Credentials.UsernamePassword("test", "test", createUser: false);
            user = await User.LoginAsync(credentials, new Uri(Constants.AuthUrl));
            var configuration = new FullSyncConfiguration(new Uri(Constants.RealmPath, UriKind.Relative), user);
            // First time the user logs in, let's use GetInstanceAsync so we fully download the Realm
            // before letting them interract with the UI.
            var realm = await Realm.GetInstanceAsync(configuration);
            return realm;
            }
        
        public AutoMainViewModel()
        {
            AddCommand = new Command(
                execute: () =>
                {
                    _realm.WriteAsync((tempRealm) =>
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
                    _realm.WriteAsync((tempRealm) =>
                    {
                        tempRealm.Add(SelectedMovie, true);
                    });
                },
                canExecute: () => SelectedMovie != null
                );
            Initialize().IgnoreResult();

        }

       public Movie SelectedMovie
        {
            get;set;        }

        
        public string NewTitle { get; set;  }

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
