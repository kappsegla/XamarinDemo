using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class CalcViewModel : BaseViewModel
    {
        public CalcViewModel()
        {
            Result = "";
            FontSize = 60;

            AddCommand = new Command(
                obj => {
                    //Execute
                    Result = "Clicked!";
            },
                obj => {
                    //Can Execute?
                    return true;
            });
            NumberCommand = new Command<string>(
                execute: NumberButton,
                canExecute: obj =>{
                    if (obj.Equals("0") && Result.Length == 1 && Result.StartsWith("0"))
                        return false;
                    else
                        return true;
                });
        }

        private void NumberButton(string obj)
        {
            if (Result.StartsWith("0") && !Result.StartsWith("0."))
                Result = obj;
            else
                Result += obj;
            RefreshCanExecute();            
        }

      
        //Tell all buttons to check there canexecute status again
        private void RefreshCanExecute()
        {
            (NumberCommand as Command).ChangeCanExecute();
            (AddCommand as Command).ChangeCanExecute();
        }

        public ICommand AddCommand { private set; get; }
        public ICommand NumberCommand { private set; get; }

        private string result;
        public string Result {
            get
            {
                return result;
            }
            set
            {
                SetProperty(ref result, value);
            }
        }

        public int FontSize { get; set; }
    }
}
