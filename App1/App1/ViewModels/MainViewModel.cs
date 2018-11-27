using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace App1.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Person> People { get; } = new ObservableCollection<Person>();

        public MainViewModel()
        {
            People.Add(new Person { Name = "One" });
            People.Add(new Person { Name = "Two" });
            People.Add(new Person { Name = "Three" });
            People.Add(new Person { Name = "Four" });
        }
        Person selectedPerson;
        public Person SelectedPerson
        {
            get
            {
                return selectedPerson;
            }

            set
            {
                SetProperty(ref selectedPerson, value);
            }
        }
    }
}
