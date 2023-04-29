using SmartLab.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartLab.ViewModels
{
    public class CreatePasswordViewModel : ViewModelBase
    {
        public CreatePasswordViewModel()
        {
            SkipCommand = new Command(BtnSkip);
            ButtonClickCommand = new Command(ButtonClick);
            ButtonEraseClickCommand = new Command(EraseCommand);
        }

        private void EraseCommand(object obj)
        {
            Password = Password.Substring(0, Password.Length - 1);
            switch (Password.Length)
            {
                case 0:
                    _firstEntryBackColor = Color.FromArgb("#FFFFFF");
                    OnPropertyChanged(nameof(FirstEntryBackColor));
                    break;
                case 1:
                    _secondEntryBackColor = Color.FromArgb("#FFFFFF");
                    OnPropertyChanged(nameof(SecondEntryBackColor));
                    break;
                case 2:
                    _thirdEntryBackColor = Color.FromArgb("#FFFFFF");
                    OnPropertyChanged(nameof(ThirdEntryBackColor));
                    break;
                case 3:
                    _fourthEntryBackColor = Color.FromArgb("#FFFFFF");
                    OnPropertyChanged(nameof(FourthEntryBackColor));
                    break;
            }
        }

        private async void ButtonClick(object sender)
        {
            Password += (sender as Button).Text;
            switch (Password.Length)
            {
                case 1:
                    _firstEntryBackColor = Color.FromArgb("#1A6FEE");
                    OnPropertyChanged(nameof(FirstEntryBackColor));
                    break;
                case 2:
                    _secondEntryBackColor = Color.FromArgb("#1A6FEE");
                    OnPropertyChanged(nameof(SecondEntryBackColor));
                    break;
                case 3:
                    _thirdEntryBackColor = Color.FromArgb("#1A6FEE");
                    OnPropertyChanged(nameof(ThirdEntryBackColor));
                    break;
                case 4:
                    _fourthEntryBackColor = Color.FromArgb("#1A6FEE");
                    OnPropertyChanged(nameof(FourthEntryBackColor));

                    Preferences.Set("UserPassword", Password);

                    await AppShell.Current.GoToAsync($"//{nameof(PatientCardView)}");
                    break;
            }
        }

        private async void BtnSkip(object obj)
        {
            await AppShell.Current.GoToAsync($"//{nameof(PatientCardView)}");
        }

        private Color _firstEntryBackColor;

        public Color FirstEntryBackColor
        {
            get { return _firstEntryBackColor; }
            set { _firstEntryBackColor = value; OnPropertyChanged(); }
        }

        private Color _secondEntryBackColor;
        public Color SecondEntryBackColor
        {
            get { return _secondEntryBackColor; }
            set { _secondEntryBackColor = value; OnPropertyChanged(); }
        }

        private Color _thirdEntryBackColor;
        public Color ThirdEntryBackColor
        {
            get { return _thirdEntryBackColor; }
            set { _thirdEntryBackColor = value; OnPropertyChanged(); }
        }

        private Color _fourthEntryBackColor;
        public Color FourthEntryBackColor
        {
            get { return _fourthEntryBackColor; }
            set { _fourthEntryBackColor = value; OnPropertyChanged(); }
        }


        public ICommand SkipCommand { get; set; }
        public ICommand ButtonClickCommand { get; set; }
        public ICommand ButtonEraseClickCommand { get; set; }

        private string _password = string.Empty;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged();
                OnPropertyChanged("BtnIsEnabled");
                OnPropertyChanged("BtnEraseIsEnabled");
            }
        }

        private bool _btnIsEnabled = true;
        public bool BtnIsEnabled
        {
            get { return Password.Length == 4 ? false : true; }
            set { _btnIsEnabled = value; OnPropertyChanged(); }
        }

        private bool _btnEraseIsEnabled;
        public bool BtnEraseIsEnabled
        {
            get { return Password.Length == 0 ? false : true; }
            set { _btnIsEnabled = value; OnPropertyChanged(); }
        }

    }
}
