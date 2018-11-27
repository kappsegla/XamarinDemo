using App1.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            // force a specific culture, useful for quick testing
            //AppResources.Culture = new CultureInfo("sv-SE");
            translateLabel.Text = AppResources.Label1Text;
        }
    }
}
